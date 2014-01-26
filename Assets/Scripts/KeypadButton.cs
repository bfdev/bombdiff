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
			renderer.material.color = new Color(232.0f / 255.0f, 124.0f / 255.0f, 38.0f / 255.0f);
		} else {
			renderer.material.color = Color.white;
		}
	}

	[ContextMenu("Force Highlight Now")]
	public void ForceHighlightNow()
	{
		SetHighlight(true);
	}

	public void Push() {
		if (parentComponent.ButtonPushed (buttonIndex)) {
			isPressed = true;
			renderer.material.color = Color.green;
		}
	}
}
