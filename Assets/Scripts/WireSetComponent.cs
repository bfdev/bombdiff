using UnityEngine;
using System.Collections;

public class WireSetComponent : MonoBehaviour
{
	public GameObject wirePrefab;
	public float spawnRange = 5;
	public int maxWires = 1;

	public SnippableWire[] wires;

	// Use this for initialization
	void Start()
	{
		int rand = (int)(Random.value * maxWires) + 1;
		if (rand == maxWires + 1)
		{
			rand -= 1;
		}

		int numberOfWires = rand;
		wires = new SnippableWire[numberOfWires];

		float zStepSize = spawnRange / (numberOfWires + 1);
		float currentZ = (spawnRange / -2.0f) + zStepSize;
		for (int i = 0; i < numberOfWires; i++)
		{
			GameObject newWire = GameObject.Instantiate(wirePrefab) as GameObject;
			newWire.transform.parent = this.transform;
			newWire.transform.localPosition = new Vector3(0, 0, currentZ);

			currentZ += zStepSize;

			wires[i] = newWire.GetComponent<SnippableWire>();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(this.transform.position, new Vector3(0.2f, 0.2f, spawnRange));
	}
}
