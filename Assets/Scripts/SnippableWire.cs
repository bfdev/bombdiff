using UnityEngine;
using System.Collections;

public class SnippableWire : MonoBehaviour
{
	public GameObject snippedWire;
	public GameObject nonSnippedWire;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Snip()
	{
		snippedWire.SetActive(true);
		nonSnippedWire.SetActive(false);
	}
}
