using UnityEngine;
using System.Collections;
using System;
using BombGame;

public class WireSetComponent : MonoBehaviour
{
	public GameObject wirePrefab;
	public float spawnRange = 5;
	public int maxWires = 1;
	public int minWires = 2;

	public SnippableWire[] wires;

	// Use this for initialization
	void Start()
	{
		int rand = (int)(UnityEngine.Random.value * maxWires) + minWires;
		int numberOfWires = Mathf.Clamp(rand, minWires, maxWires);

		wires = new SnippableWire[numberOfWires];

		float zStepSize = spawnRange / (numberOfWires + 1);
		float currentZ = (spawnRange / -2.0f) + zStepSize;
		for (int i = numberOfWires - 1; i >= 0; i--)
		{
			GameObject newWire = GameObject.Instantiate(wirePrefab) as GameObject;
			newWire.transform.parent = this.transform;
			newWire.transform.localPosition = new Vector3(0, 0, currentZ);

			currentZ += zStepSize;

			wires[i] = newWire.GetComponent<SnippableWire>();
			wires[i].WireIndex = i;
			wires[i].ParentComponent = this;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnSnip(int indexOfCutWire)
	{
		int correctWire = GetSolutionIndex();

		if (indexOfCutWire == correctWire)
		{
			//Yay!
			Debug.Log(String.Format ("Correct wire snipped: {0}!", indexOfCutWire));
		}
		else
		{
			//Uh oh! Boooooom!
			Debug.Log(String.Format ("Wrong wire snipped: {0} but solution is {1}!", indexOfCutWire, correctWire));
			SceneManager.Instance.Bomb.OnStrike();
		}
	}

	//Based on the state of the bomb, calculate the correct wire index to cut
	protected int GetSolutionIndex()
	{
		String solution = "";

		int wireCount = wires.Length;
		int solutionIndex = -1;

		if (wireCount >= 4) 
		{
			solution += "More than 3 wires, ";

			if (GetColourCount(WireColour.Blue) == 1)
			{
				solution += "exactly one blue wire, cut index 2.";
				solutionIndex = 2;
			}
			else
			{
				solution += "blue wire count != 1, cut index 1.";
				solutionIndex = 1;
			}
		}
		else if (wireCount >= 1)
		{
			solution += "Less than 4 wires but more than 0, ";
			if (GetColourCount(WireColour.Yellow) > 0)
			{
				int indexOfYellow = GetFirstIndexOfColour(WireColour.Yellow);

				solution += "at least one yellow wire, cut index of first yellow is:" + indexOfYellow;
				solutionIndex = indexOfYellow;
			}
			else if (SceneManager.Instance.Bomb.Serial != null)
			{
				solution += "no yellow wires, ";

				SerialNumber sn = SceneManager.Instance.Bomb.Serial;

				if (char.IsLetter(sn.GetSerialString()[0]))
				{
					solution += "serial starts with a letter, cut last wire.";
					solutionIndex = wireCount - 1; //last wire
				}
				else if (sn.IsLastDigitEven())
				{
					solution += "serial does not start with letter, ends with even number, cut first wire.";
					solutionIndex = 0;
				}
				else
				{
					solution += "serial does not start with letter, ends with odd number, cut last wire.";
					solutionIndex = wireCount - 1; //last wire
				}
			}
			else
			{
				solution += "no yellow wires, no serial number, cut index 0";
				solutionIndex = 0;
			}
		}


		Debug.Log(solution);
		return solutionIndex;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(this.transform.position, new Vector3(0.2f, 0.2f, spawnRange));
	}

	public int GetColourCount(WireColour colour)
	{
		int count = 0;
		
		foreach(SnippableWire wire in wires)
		{
			if (wire.getColour() == colour)
			{
				count++;
			}
		}
		
		return count;
	}

	public int GetFirstIndexOfColour(WireColour colour)
	{
		for(int index = 0; index < wires.Length; index++)
		{
			if (wires[index].getColour() == colour)
			{
				return index;
			}
		}

		//Uh oh, no wire of that colour!
		Debug.Log(String.Format("Tried to get index of Colour {0} wire, but none exists.", colour));
		return -1;
	}
}
