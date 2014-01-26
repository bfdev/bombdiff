using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

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
	public AudioClip Victory;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		bool loadedSettings = TryLoadSettings();

		if (!loadedSettings)
		{
			SaveSettingsIfNoneExist();
		}

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

	protected bool TryLoadSettings()
	{
		Debug.Log ("Attempting to load settings...");

		try
		{
			XmlSerializer serializer = new XmlSerializer(typeof(GameSettings));
			FileStream file = new FileStream("config.xml", FileMode.Open);
			GameSettings settings = (GameSettings) serializer.Deserialize(file);

			SecondsToSolve = settings.SecondsToSolve;
			NumStrikesToLose = settings.NumStrikesToLose;
			NumComponentsToSolve = settings.Difficulty;

			Debug.Log ("Successfully loaded settings.");

			return true;
		}
		catch(Exception e)
		{
			//meh
			Debug.Log ("Failed to load settings: " + e.Message);
		}

		return false;
    }

	protected void SaveSettingsIfNoneExist()
	{
		try
		{
			XmlSerializer serializer = new XmlSerializer(typeof(GameSettings));
			FileStream file = new FileStream("config.xml", FileMode.CreateNew);
			serializer.Serialize(file, new GameSettings());

			Debug.Log ("Successfully saved settings.");
		}
		catch(Exception e)
        {
            //meh
            Debug.Log ("Failed to save settings: " + e.Message);
		}
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

