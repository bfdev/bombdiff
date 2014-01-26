using UnityEngine;
using System.Collections;

public class BombGenerator : MonoBehaviour
{
	public GameObject[] bombPrefabs;
	public GameObject[] solvableComponentPrefabs;
	public GameObject[] unsolvableComponentPrefabs;
	public GameObject[] requiredComponentPrefabs;

	public GameObject SerialNumberPrefab;

	public float SerialNumberChance;

	protected System.Random rand = new System.Random();

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public Bomb CreateBomb(int numStrikesToFail)
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
			bombScript.BombComponents.Add(newComponent);

			//remove this component from the newComponents now that it's added...
			if (newComponents.Length > 1)
			{
				GameObject[] smallerArray = new GameObject[newComponents.Length - 1];
				int newComponentI = 0;
				for (int i = 0; i < smallerArray.Length; i++)
				{
					if (i == newComponentI && newComponents[i] == randomComponent)
					{
						// Skip this one, 'cause it's the one we just added
						newComponentI++;
					}
					smallerArray[i] = newComponents[newComponentI];
					newComponentI++;
				}
				newComponents = smallerArray;
			}
			else
			{
				break; // we were going to be breaking anyway, but whatever
			}
		}


		if (rand.NextDouble() < SerialNumberChance)
		{
			if (bombScript.SerialNumberAnchorPoints.Length > 0)
			{
				Transform serialAnchorPoint = bombScript.SerialNumberAnchorPoints[rand.Next(0, bombScript.SerialNumberAnchorPoints.Length)];
				GameObject serialGO = GameObject.Instantiate(SerialNumberPrefab, 
				                                             serialAnchorPoint.position, 
				                                             serialAnchorPoint.rotation) as GameObject;
				serialGO.transform.parent = bombScript.visualTransform;
				bombScript.Serial = serialGO.GetComponent<SerialNumber>();
			}
		}


		//Add the status lights
		Timer timer = bombScript.GetTimer();
		if (timer != null)
		{
			numStrikesToFail = Mathf.Clamp(numStrikesToFail, 2, Mathf.Min(timer.StatusLightAnchors.Length + 1, 5));
			bombScript.NumStrikesToLose = numStrikesToFail;

			int numStatusLights = numStrikesToFail - 1;

			for (int i = 0; i < numStatusLights; i++)
			{
				//get the next status light anchor and instantiate a light there
				Transform statusLightAnchorPoint = timer.StatusLightAnchors[i];
				GameObject statusLightGO = GameObject.Instantiate(timer.StatusLightPrefab, 
				                                                  statusLightAnchorPoint.position, 
				                                                  statusLightAnchorPoint.rotation) as GameObject;
				statusLightGO.transform.parent = timer.transform;

				bombScript.StatusLights.Add(statusLightGO.GetComponent<StatusLight>());
            }
		}


		return bombScript;
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
