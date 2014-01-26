using UnityEngine;
using System.Collections;

public class BombGenerator : MonoBehaviour
{
	public GameObject[] bombPrefabs;
	public GameObject[] solvableComponentPrefabs;
	public GameObject[] unsolvableComponentPrefabs;
	public GameObject[] requiredComponentPrefabs;

	// Use this for initialization
	void Start()
	{
		CreateBomb();
	}

	// Update is called once per frame
	void Update()
	{

	}

	protected void CreateBomb()
	{
		GameObject bombPrefab = randomGOFromArray(bombPrefabs);
		GameObject newBomb = GameObject.Instantiate(bombPrefab, new Vector3(-0.03f, 0.9f, -0.42f), Quaternion.identity) as GameObject;
		Bomb bombScript = newBomb.GetComponent<Bomb>();

		GameObject[] allComponents = new GameObject[solvableComponentPrefabs.Length + unsolvableComponentPrefabs.Length];
		for(int i = 0; i < allComponents.Length; i++)
		{
			if(i < solvableComponentPrefabs.Length)
			{
				allComponents[i] = solvableComponentPrefabs[i];
			}
			else
			{
				allComponents[i] = unsolvableComponentPrefabs[i - solvableComponentPrefabs.Length];
			}
		}

		// Intermediate component set that has bad ordering
		GameObject[] newComponents = new GameObject[bombScript.componentAnchorPoints.Length];
		for (int i = 0; i < newComponents.Length; i++)
		{
			// Add required components
			if(i < requiredComponentPrefabs.Length)
			{
				newComponents[i] = requiredComponentPrefabs[i];
			}
			// First non-required component should have a solution
			else if (i == requiredComponentPrefabs.Length)
			{
				GameObject newComponent = randomGOFromArray(solvableComponentPrefabs);
				newComponents[i] = newComponent;
			}
			// The rest are totally random
			else
			{
				GameObject newComponent = randomGOFromArray(allComponents);
				newComponents[i] = newComponent;
			}
		}

		// Take the temporary component set and randomly assign them to the anchor points
		foreach (Transform anchorPoint in bombScript.componentAnchorPoints)
		{
			GameObject randomComponent = randomGOFromArray(newComponents);
			GameObject newComponent = GameObject.Instantiate(randomComponent, anchorPoint.position, anchorPoint.rotation) as GameObject;
			newComponent.transform.parent = bombScript.visualTransform;

			//remove this component from the newComponents now that it's added...
			if (newComponents.Length > 1)
			{
				GameObject[] smallerArray = new GameObject[newComponents.Length - 1];
				for (int i = 0; i < smallerArray.Length; i++)
				{
					smallerArray[i] = newComponents[i];
				}
				newComponents = smallerArray;
			}
			else
			{
				break; // we were going to be breaking anyway, but whatever
			}
		}
	}

	protected GameObject randomGOFromArray(GameObject[] array)
	{
		int rand = (int)(Random.value * array.Length);
		if (rand == array.Length)
		{
			rand -= 1;
		}
		return array[rand];
	}
}
