using UnityEngine;
using System.Collections;

public class KeypadButton : MonoBehaviour {
	public TextMesh text;
	public KeypadComponent parentComponent;
	public int buttonIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetText(string buttonText) {
		text.text = buttonText;
	}

	public string GetText() {
		return text.text;
	}

	public void SetHighlight(bool highlighted) {
		if(highlighted) {
			renderer.material.color = Color.gray;
		} else {
			renderer.material.color = Color.white;
		}
	}

	public void Push() {
		parentComponent.ButtonPushed (buttonIndex);
	}
}
