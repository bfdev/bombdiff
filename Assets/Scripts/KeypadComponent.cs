using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeypadComponent : MonoBehaviour {
	public KeypadButton[] buttons;
	string[] symbols = {"©","★","☆","ئ","ټ","Җ","Ω","Ѭ","Ѽ"};

	// Use this for initialization
	void Start () {
		List<string> symbolsClone = new List<string>(symbols);
		for(int i = 0; i < buttons.Length; i++) {
			KeypadButton button = buttons[i];
			int rand = (int)(Random.value * symbolsClone.Count);
			button.SetText(symbolsClone[rand]);
			button.parentComponent = this;
			button.buttonIndex = i;
			symbolsClone.RemoveAt (rand);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonPushed(int index) {
		
	}
}
