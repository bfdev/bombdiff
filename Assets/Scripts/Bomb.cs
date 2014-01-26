using UnityEngine;
using System.Collections.Generic;

public class Bomb : MonoBehaviour
{
	public Transform[] componentAnchorPoints;
	public Transform[] SerialNumberAnchorPoints;
	public Transform visualTransform;

	public List<GameObject> BombComponents = new List<GameObject>();
	public SerialNumber Serial;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
