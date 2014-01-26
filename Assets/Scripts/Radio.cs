using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour {
	public AudioSource[] music;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayMusic() {
		AudioSource source = GetNextSource ();
		Invoke ("PlayMusic", source.clip.length);
		source.enabled = true;
		source.Play ();
	}

	public void StopMusic()
	{
		foreach(AudioSource audioSrc in music)
		{
			audioSrc.Stop();
		}
	}

	AudioSource GetNextSource() {
		float timeRemaining = SceneManager.Instance.Bomb.GetTimer().timeRemaining;
		float timeTotal = SceneManager.Instance.SecondsToSolve;
		int index = (int)(music.Length - (timeRemaining / timeTotal * music.Length));
		index = Mathf.Clamp(index, 0, music.Length - 1);
		return music [index];
	}
}
