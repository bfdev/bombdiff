using UnityEngine;
using System.Collections;

public class DialingWand : HandTool {
	KeypadButton m_button;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void Activate() {
		if (m_button) {
			m_button.Push ();
		}
	}
	
	void OnTriggerEnter (Collider col){
		
		if (col.tag == "KeypadButton" && !m_button) {
			m_button = col.gameObject.GetComponent<KeypadButton>();
			m_button.SetHighlight(true);
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (m_button && col.gameObject == m_button.gameObject) {
			
			m_button.SetHighlight(false);
			m_button = null;
		}
	}
}
