using UnityEngine;
using System.Collections;

public class Wirecutters : HandTool {
	SnippableWire m_wire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Activate() {
		m_wire.Snip();
	}

	void OnTriggerEnter (Collider col){
		if (col.tag == "Wire") {
			m_wire = col.gameObject.GetComponent<SnippableWire>();
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (col.gameObject == m_wire) {
			m_wire = null;
		}
	}
}
