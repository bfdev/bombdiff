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

	protected int numPasses;
	protected int numPassesToWin;

	protected bool hasDetonated;
	public Detonator detonator;

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
			Detonate();
		}
		else
		{
			//make a sound
			
			//turn on the next light
			if (NumStrikes < StatusLights.Count)
			{
				StatusLights[NumStrikes].SetStrike(true);
			}

			switch(NumStrikes)
			{
				case 0:
					{
						//first strike
						//lose 30 seconds
						GetTimer().SubtractTime(30);
					}
					break;
				case 1:
					{
						//second strike
						//speed up the clock
						GetTimer().SetRateModifier(1.5f);
					}
					break;
				case 2:
					{
						//third strike
						GetTimer().SetRateModifier(3.0f);
					}
					break;
				case 3:
					{
						//fourth strike
						GetTimer().SetRateModifier(6.0f);
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
		numPasses++;

		int numPassesToWin = 0;

		foreach(var comp in BombComponents)
		{
			if (comp.GetComponent<WireSetComponent>() != null ||
			    comp.GetComponent<KeypadComponent>() != null)
			{
				numPassesToWin++;
			}
		}

		if (numPasses >= numPassesToWin)
		{
			GameWon();
		}
	}

	protected void GameWon()
	{
		Debug.Log ("A winner is you!!");
		GetTimer().StopTimer();
		foreach(StatusLight statusLight in StatusLights)
		{
			statusLight.SetPass();
        }
		audio.PlayOneShot(SceneManager.Instance.Victory);
		SceneManager.Instance.radio.StopMusic();
    }
    
	public void Detonate()
	{
		if (!hasDetonated)
		{
			detonator.Explode ();
			Debug.Log("boom");
			hasDetonated = true;
			GetTimer().StopTimer();
			SceneManager.Instance.radio.StopMusic();
			foreach(StatusLight statusLight in StatusLights)
			{
				statusLight.SetInactive();
			}

			foreach(var hand in GameObject.FindObjectsOfType(typeof(SixenseHandController)))
			{
				((SixenseHandController)hand).m_pickupZone.Drop();
				((SixenseHandController)hand).gameObject.SetActive(false);
			}

			SceneManager.Instance.StartFade();
		}
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
