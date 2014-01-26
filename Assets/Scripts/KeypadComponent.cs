using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeypadComponent : MonoBehaviour {
	public KeypadButton[] buttons;
	string[] symbols = {"©","★","☆","ئ","ټ","Җ","Ω","Ѭ","Ѽ"};
	List<string> solutionOrder;
	
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
		solutionOrder = GetSolutionOrder ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public bool ButtonPushed(int index) {
		KeypadButton pressedButton = buttons [index];
		int solutionValue = solutionOrder.IndexOf (pressedButton.GetText ());
		foreach(KeypadButton button in buttons) {
			int otherSolVal = solutionOrder.IndexOf (button.GetText ());
			if(!button.isPressed && otherSolVal < solutionValue) {
				return false;
			}
		}

		return true;
	}
	
	List<string> GetSolutionOrder() {
		List<string> solutionOrder = new List<string>(new string[]{"©","★","☆","ئ","ټ","Җ","Ω","Ѭ","Ѽ"});
		SerialNumber sn = SceneManager.Instance.Bomb.Serial;
		//Serial number starts with number
		/*if (!char.IsLetter(sn.GetSerialString()[0]))
		{
			return new List<string>(new string[]{"Җ","Ѽ","★","©","ئ","☆","ټ","Ω","Ѭ"});
		}*/
		
		foreach (KeypadButton button in buttons) {
			if(button.GetText() == "Ѭ") {
				return new List<string>(new string[]{"Ѭ","Җ","©","Ѽ","★","Ω","ئ","☆","ټ"});
			}
		}
		
		foreach (KeypadButton button in buttons) {
			if(button.GetText() == "Җ") {
				return new List<string>(new string[]{"Ѽ","★","Ω","ئ","☆","Ѭ","Җ","©","ټ"});
			}
		}
		
		return solutionOrder;
	}
}