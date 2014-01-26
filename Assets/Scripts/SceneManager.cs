using System;
using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	public static SceneManager Instance;

	public BombGenerator BombGenerator;
	public int NumStrikesToLose;
	public float SecondsToSolve;
	public int NumComponentsToSolve;
	public Radio radio;

	public Bomb Bomb;

	public GameObject Lightbulb;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		//Eventually options and game flow will go here
		Lightbulb.SetActive(false);
		Bomb = BombGenerator.CreateBomb(NumStrikesToLose, NumComponentsToSolve);
		Bomb.GetTimer().SetTimeRemaing(SecondsToSolve);
		Bomb.GetTimer().text.renderer.enabled = false;
		Bomb.GetTimer().light.enabled = false;
		SetAllTextRenderers(false);
		radio.PlayMusic();
        
		StartCoroutine(StartRound());
    }

	protected void SetAllTextRenderers(bool toggle)
	{
		foreach(var text in Bomb.GetComponentsInChildren<TextMesh>())
		{
			text.renderer.enabled = toggle;
		}
	}

	protected IEnumerator StartRound()
	{
		radio.GetComponent<RecordPlayer>().recordPlayerActive = true;

        yield return new WaitForSeconds(5);
		Bomb.GetTimer().text.renderer.enabled = true;
		Bomb.GetTimer().light.enabled = true;
		yield return new WaitForSeconds(5);
		SetAllTextRenderers(true);
		Lightbulb.SetActive(true);
		Lightbulb.audio.Play();
		yield return new WaitForSeconds(3);
		Bomb.GetTimer().StartTimer();

	}
}

