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

	public override void Activate() {
		if (m_wire) {
			m_wire.Snip ();
		}
	}

	void OnTriggerEnter (Collider col){

		if (col.tag == "Wire" && !m_wire) {
			m_wire = col.gameObject.GetComponent<SnippableWire>();
			m_wire.setIsHighlighted(true);
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (m_wire && col.gameObject == m_wire.gameObject) {

			m_wire.setIsHighlighted(false);
			m_wire = null;
		}
	}
}
