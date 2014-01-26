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

	public Material blackMaterialHighlight;
	public Material blueMaterialHighlight;
	public Material redMaterialHighlight;
	public Material whiteMaterialHighlight;
	public Material yellowMaterialHighlight;

	public GameObject snippedWire;
	public GameObject nonSnippedWire;

	protected bool _isHighlighted = false;
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
		if (_isHighlighted)
		{
			switch (newColour)
			{
				case WireColour.Black:
					newMaterial = blackMaterialHighlight;
					break;
				case WireColour.Blue:
					newMaterial = blueMaterialHighlight;
					break;
				case WireColour.Red:
					newMaterial = redMaterialHighlight;
					break;
				case WireColour.White:
					newMaterial = whiteMaterialHighlight;
					break;
				case WireColour.Yellow:
				default:
					newMaterial = yellowMaterialHighlight;
					break;
			}
		}
		else
		{
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

	public void setIsHighlighted(bool value)
	{
		_isHighlighted = value;
		// reset material to use the highlighted material or non-highlighted
		setColour(_colour);
	}

	// For testing highlight...
	//void OnMouseEnter()
	//{
	//	setIsHighlighted(true);
	//}

	//void OnMouseExit()
	//{
	//	setIsHighlighted(false);
	//}

}
