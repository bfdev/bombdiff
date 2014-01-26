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

	// Use this for initialization
	void Start () {
		timeRemaining = 120.0f;
		lastBeep = 0.0f;
		UpdateDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Floor (timeRemaining - Time.deltaTime) < Mathf.Floor (timeRemaining)) {
			UpdateDisplay ();
		}
		timeRemaining -= Time.deltaTime;
		if (timeRemaining > 15) {
			if (lastBeep > 1.0f) {
				lastBeep = 0.0f;
				audio.Play ();
			}
		} else {
			if (lastBeep > 0.15f) {
				fastBeep.Play ();
				lastBeep = 0.0f;
			}
		}
		lastBeep += Time.deltaTime;
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
