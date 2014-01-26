using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour {
	float timeRemaining;
	float lastBeep;
	public TextMesh text;
	public AudioSource fastBeep;

	public Transform[] StatusLightAnchors;
	public GameObject StatusLightPrefab;

	protected bool isUpdating = true;
	protected float rateModifier = 1f;

	// Use this for initialization
	void Start () 
	{
		lastBeep = float.MaxValue;
		UpdateDisplay ();
		isUpdating = false;
	}

	public void SetTimeRemaing(float time)
	{
		timeRemaining = Mathf.Max (0, time);
		UpdateDisplay ();
	}

	public void SubtractTime(float deltaTime)
	{
		timeRemaining = Mathf.Max (0, timeRemaining - deltaTime);
		UpdateDisplay ();
	}

	public void SetRateModifier(float newRate)
	{
		rateModifier = newRate;
	}

	public void StartTimer()
	{
		isUpdating = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isUpdating)
		{
			return;
		}

		//if (Mathf.Floor (timeRemaining - Time.deltaTime) < Mathf.Floor (timeRemaining)) 
		{
			UpdateDisplay ();
		}

		timeRemaining -= (Time.deltaTime * rateModifier);

		if (timeRemaining > 30) 
		{
			//Debug.Log (String.Format("last:{0} timeRemaining:{1} dif: {2}", lastBeep, timeRemaining, lastBeep - timeRemaining));
			if (lastBeep - timeRemaining > 1.0f) 
			{
				lastBeep = timeRemaining;
				audio.Play ();
			}
		}
		else
		{
			if (lastBeep - timeRemaining > 0.5f) 
			{
				lastBeep = timeRemaining;
				audio.Play ();
			}
		}

		if (timeRemaining <= 0)
		{
			SceneManager.Instance.Bomb.Detonate();
			isUpdating = false;
		}
	}

	void UpdateDisplay() {
		if (text) {
			var span = new TimeSpan(0, 0, (int)Mathf.Floor (timeRemaining)); //Or TimeSpan.FromSeconds(seconds); (see Jakob C´s answer)
			var fmtString = string.Format("{0}:{1:00}", 
			                            (int)span.TotalMinutes, 
			                            span.Seconds);
			text.text = fmtString;
		}
	}
}
