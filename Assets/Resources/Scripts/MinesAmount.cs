using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesAmount : MonoBehaviour {
	int[] numbers = new int[4];
	int numbersCount = 0;
	public int minesAmount;

	GameObject[] goNumbers = new GameObject[4];

	// Use this for initialization
	void Start () {
		minesAmount = (PlayerPrefs.GetString("isCustom") == "true") ? PlayerPrefs.GetInt("MinesAmountC") : PlayerPrefs.GetInt("MinesAmount");

		updateMinesAmount(0);
	}

	void showSprites()
	{	
		for (int i = 3; i >= numbersCount; i--)
			goNumbers[i].SetActive(false);

		for (int i = 0; i < 4; i++)
			goNumbers[i].GetComponent<SpriteRenderer>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().numbers[numbers[i]];

	}

	public void updateMinesAmount(int value)
	{
		for (int i = 0; i < 4; i++)
			goNumbers[i] = transform.GetChild(i).gameObject;

		for (int i = 3; i >= numbersCount; i--)
			goNumbers[i].SetActive(true);

		minesAmount += value;
		splitTheNumbers();
		showSprites();
	} 

	void splitTheNumbers()
	{
		numbersCount = 0;
		int localMinesAmount = minesAmount;

		do{
			numbers[numbersCount] = localMinesAmount % 10;
			numbersCount++;
			localMinesAmount /= 10;
		}while (localMinesAmount != 0);

		int localNumbersCount = numbersCount;
		localMinesAmount = minesAmount;

		do{
			numbers[--localNumbersCount] = localMinesAmount % 10;
			localMinesAmount /= 10;
		}while (localMinesAmount != 0);
	}
}
