using UnityEngine;
using System.Collections;

public class SerialNumber : MonoBehaviour 
{
	public int MaxSerialLength = 6;
	protected TextMesh serialTextMesh;
	protected string serialString;

	protected char[] possibleCharArray;

	void Awake()
	{
		serialTextMesh = GetComponent<TextMesh>();

		possibleCharArray = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 
			'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9' 
		};
	}

	void Start()
	{
		System.Random rand = new System.Random();

		string randomSerial = "";
		for(int i = 0; i < MaxSerialLength - 1; i++)
		{
			randomSerial += possibleCharArray[rand.Next(0, possibleCharArray.Length)];
		}

		//Last character is always a digit
		randomSerial += rand.Next(0, 9);

		SetSerialNumber(randomSerial);
	}

	public void SetSerialNumber(string serial)
	{
		if (serial.Length > MaxSerialLength)
		{
			serial = serial.Substring(0, MaxSerialLength);
		}

		serialTextMesh.text = serial;
		serialString = serial;

		//resize the backing plate
	}

	public string GetSerialString()
	{
		return serialString;
	}

	public bool IsLastDigitEven()
	{
		return System.Int32.Parse(serialString.Substring(serialString.Length - 1, 1)) % 2 == 0;
	}
}
