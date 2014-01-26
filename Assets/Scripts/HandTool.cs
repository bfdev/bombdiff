using UnityEngine;
using System.Collections;

public class HandTool : MonoBehaviour {
	static GameObject[] toolPrefabs = {(GameObject)Resources.Load ("Wirecutters")};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Activate() {
	}

	public static HandTool GetTool(int tool) {
		HandTool handTool = null;
		if (tool >= toolPrefabs.Length) {
			return null;
		}

		GameObject newTool = GameObject.Instantiate(toolPrefabs[tool]) as GameObject;
		handTool = newTool.GetComponent<HandTool>();

		return handTool;
	}

	public static int GetNumTools() {
		return toolPrefabs.Length;
	}
}