using UnityEngine;
using System.Collections;

public class SnippableWire : MonoBehaviour
{
	public enum WireColour
	{
		Black,
		Blue,
		Red,
		White,
		Yellow
	}
	public Material blackMaterial;
	public Material blueMaterial;
	public Material redMaterial;
	public Material whiteMaterial;
	public Material yellowMaterial;

	public GameObject snippedWire;
	public GameObject nonSnippedWire;

	protected WireColour _colour;

	// Use this for initialization
	void Start()
	{
		int numColours = 5;
		int rand = (int)(Random.value * numColours);
		if (rand == numColours)
		{
			rand -= 1;
		}
		setColour((WireColour)rand);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void setColour(WireColour newColour)
	{
		Material newMaterial;
		switch (newColour)
		{
			case WireColour.Black:
				newMaterial = blackMaterial;
				break;
			case WireColour.Blue:
				newMaterial = blueMaterial;
				break;
			case WireColour.Red:
				newMaterial = redMaterial;
				break;
			case WireColour.White:
				newMaterial = whiteMaterial;
				break;
			case WireColour.Yellow:
			default:
				newMaterial = yellowMaterial;
				break;
		}
		foreach (MeshRenderer meshRenderer in this.gameObject.GetComponentsInChildren<MeshRenderer>())
		{
			meshRenderer.material = newMaterial;
		}
		_colour = newColour;
	}

	public WireColour getColour()
	{
		return _colour;
	}

	public void Snip()
	{
		snippedWire.SetActive(true);
		nonSnippedWire.SetActive(false);
	}

	public bool isSnipped()
	{
		return nonSnippedWire.activeInHierarchy;
	}
}
