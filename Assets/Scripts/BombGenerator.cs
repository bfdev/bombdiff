using UnityEngine;
using System.Collections;

public class BombGenerator : MonoBehaviour
{
	public GameObject[] bombPrefabs;
	public GameObject[] componentPrefabs;

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
		int rand = (int)(Random.value * bombPrefabs.Length);
		if(rand == bombPrefabs.Length)
		{
			rand -= 1;
		}
		GameObject newBomb = GameObject.Instantiate(bombPrefabs[rand]) as GameObject;
		Bomb bombScript = newBomb.GetComponent<Bomb>();

		foreach(Transform anchorPoint in bombScript.componentAnchorPoints)
		{
			rand = (int)(Random.value * componentPrefabs.Length);
			if (rand == bombPrefabs.Length)
			{
				rand -= 1;
			}
			GameObject newComponent = GameObject.Instantiate(componentPrefabs[rand], anchorPoint.position, anchorPoint.rotation) as GameObject;
			newComponent.transform.parent = newBomb.transform;
		}
	}
}
