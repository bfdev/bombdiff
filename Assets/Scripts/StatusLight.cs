using UnityEngine;
using System.Collections;

public class StatusLight : MonoBehaviour 
{
	public Material InactiveMaterial;
	public Material StrikeMaterial;
	public Material PassMaterial;
	public float PanicBlinkRate;

	void Start()
	{
	}

	public void SetStrike(bool playAudio)
	{
		this.light.enabled = true;
		this.light.color = Color.red;
		this.renderer.material = StrikeMaterial;

		if (playAudio)
		{
			audio.Play();
		}
	}

	public void SetPass()
	{
		StopAllCoroutines();
		this.light.color = Color.green;
		this.renderer.material = PassMaterial;
	}

	[ContextMenu("PanicTest")]
	public void StartPanic()
	{
		StartCoroutine(BlinkRoutine());
	}

	protected IEnumerator BlinkRoutine()
	{
		while(true)
		{
			SetStrike(false);
			yield return new WaitForSeconds(PanicBlinkRate);
			SetInactive();
			yield return new WaitForSeconds(PanicBlinkRate);
		}
	}

	public void SetInactive()
	{
		this.light.enabled = false;
		this.renderer.material = InactiveMaterial;
	}
}
