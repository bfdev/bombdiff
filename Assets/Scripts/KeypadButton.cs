using UnityEngine;
using System.Collections;

public class KeypadButton : MonoBehaviour {
	public TextMesh text;
	public KeypadComponent parentComponent;
	public int buttonIndex;
	public bool isPressed;

	// Use this for initialization
	void Start () {
		isPressed = false;
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
		if (isPressed) {
			return;
		}

		if(highlighted) {
			renderer.material.color = Color.gray;
		} else {
			renderer.material.color = Color.white;
		}
	}

	public void Push() {
		if (parentComponent.ButtonPushed (buttonIndex)) {
			isPressed = true;
			renderer.material.color = Color.green;
		}
	}
}
