using UnityEngine;
using System.Collections;
using System;
using BombGame;

public class WireSetComponent : MonoBehaviour
{
	public GameObject wirePrefab;
	public float spawnRange = 5;
	public int maxWires = 6;
	public int minWires = 3;

	public SnippableWire[] wires;

	protected bool passed;

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
			if (!passed)
			{
				//Yay!
				Debug.Log(String.Format ("Correct wire snipped: {0}!", indexOfCutWire));
				SceneManager.Instance.Bomb.OnPass();
				passed = true;
			}
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
		int wireCount = wires.Length;
		int solutionIndex = -1;

		SerialNumber sn = SceneManager.Instance.Bomb.Serial;

		String solution = String.Format("{0} wires, ", wireCount);

		switch(wireCount)
		{
			case 3:
			{
				if (sn != null && !sn.IsLastDigitEven())
				{
					solution += "serial number is odd, cut 2nd wire. ";
					solutionIndex = 1;
				}
				else if (GetColourCount(WireColour.Yellow) > 0)
				{
					solution += "at least one yellow wire, cut last yellow wire. ";
					solutionIndex = GetLastIndexOfColour(WireColour.Yellow);
				}
				else
				{
					solution += "no other rules apply, cut 1st wire. ";
					solutionIndex = 0;
				}
			}
			break;

			case 4:
			case 5:
			{
				if (GetColourCount(WireColour.Black) == 1)
				{
					solution += "exactly one black wire, cut it. ";
					solutionIndex = GetFirstIndexOfColour(WireColour.Black);
				}
				else if (sn != null && char.IsDigit(sn.GetSerialString()[0]))
				{
					solution += "serial number starts with a digit, cut 2nd wire. ";
					solutionIndex = 1;
				}
				else if (wires[0].getColour() == WireColour.Yellow ||
				         wires[0].getColour() == WireColour.Blue)
				{
					solution += "last wire is yellow or blue, cut 3rd wire. ";
					solutionIndex = 2;
				}
				else 
				{
					solution += "no other rules apply, cut 4th wire. ";
					solutionIndex = 3;
				}
			}
			break;

			case 6:
			{
				if (sn != null && !sn.IsLastDigitEven() && GetColourCount(WireColour.Red) > 0)
				{
					solution += "serial number is odd and there is at least one red wire, cut 1st wire. ";
					solutionIndex = 0;
				}
				else if (GetColourCount(WireColour.Black) > GetColourCount(WireColour.Yellow))
				{
					solution += "there are more black wires than yellow wires, cut 2nd wire. ";
					solutionIndex = 1;
				}
				else if (GetColourCount(WireColour.Black) == GetColourCount(WireColour.Blue))
				{
					solution += "there are as many black wires as blue wires, cut 3rd wire. ";
					solutionIndex = 2;
				}
				else
				{
					solution += "no other rules apply, cut 4th wire. ";
					solutionIndex = 3;
				}
			}
			break;
		}

		solution += "solutionIndex is " + solutionIndex;

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

		Debug.Log (String.Format ("There are {0} {1} wires.", count, colour));
		
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

	public int GetLastIndexOfColour(WireColour colour)
	{
		int index = -1;
		for(int i = 0; i < wires.Length; i++)
		{
			if (wires[i].getColour() == colour)
			{
				index = i;
			}
		}
		
		return index;
	}
}
