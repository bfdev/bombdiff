using UnityEngine;
using System.Collections.Generic;

public class Bomb : MonoBehaviour
{
	public Transform[] componentAnchorPoints;
	public Transform[] SerialNumberAnchorPoints;
	public Transform visualTransform;

	public List<GameObject> BombComponents = new List<GameObject>();
	public SerialNumber Serial;

	public List<StatusLight> StatusLights = new List<StatusLight>();

	public int NumStrikesToLose;
	public int NumStrikes;

	public int NumPassesToWin;
	public int NumPasses;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	//Called by a component when the player makes an incorrect move
	public void OnStrike()
	{
		Debug.Log(string.Format("Strike! {0} / {1} strikes", NumStrikes + 1, NumStrikesToLose));
		if (NumStrikes == NumStrikesToLose - 1)
		{
			//game over man!
		}
		else
		{
			//make a sound
			
			//turn on the next light
			if (NumStrikes < StatusLights.Count)
			{
				StatusLights[NumStrikes].SetStrike();
			}

			switch(NumStrikes)
			{
				case 0:
					{
						//first strike
						//maybe just make a sound?
					}
					break;
				case 1:
					{
						//second strike
					}
					break;
				case 2:
					{
						//third strike
					}
					break;
				case 4:
					{
						//fourth strike
					}
					break;
			}

			NumStrikes++;

			if (NumStrikes == NumStrikesToLose - 1)
			{
				foreach(StatusLight statusLight in StatusLights)
				{
					statusLight.StartPanic();
				}
			}
		}
	}

	//Called by a component when the player correctly solves it
	public void OnPass()
	{
		
	}

	public Timer GetTimer()
	{
		foreach(var comp in BombComponents)
		{
			Timer t = comp.GetComponentInChildren<Timer>();
			if (t != null)
			{
				return t;
			}
		}

		return null;
	}
}
