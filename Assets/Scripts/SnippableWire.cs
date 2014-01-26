using UnityEngine;
using System.Collections;
using BombGame;

public class SnippableWire : MonoBehaviour
{
	public Material blackMaterial;
	public Material blueMaterial;
	public Material redMaterial;
	public Material whiteMaterial;
	public Material yellowMaterial;

	public GameObject snippedWire;
	public GameObject nonSnippedWire;

	public WireSetComponent ParentComponent;

	public int WireIndex;

	protected WireColour _colour;
	protected bool isSnipped;


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

	[ContextMenu("Snip")]
	public void Snip()
	{
		if (!isSnipped)
		{
			snippedWire.SetActive(true);
			nonSnippedWire.SetActive(false);

			//Let's set the color again now!
			setColour(_colour);

			ParentComponent.OnSnip(WireIndex);
			isSnipped = true;
		}
	}

	public bool IsSnipped()
	{
		return isSnipped;
	}
}
