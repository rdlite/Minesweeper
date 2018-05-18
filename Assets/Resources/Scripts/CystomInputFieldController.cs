using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CystomInputFieldController : MonoBehaviour {

	void Start()
	{
		GameObject startCustom = GameObject.Find("StartCustom");

		string GOParent = gameObject.transform.parent.name;

		if (GOParent == "Width")
		{
			gameObject.GetComponent<InputField>().text = (PlayerPrefs.HasKey("WidthC")) ? PlayerPrefs.GetInt("WidthC").ToString() : 5+"";
			startCustom.GetComponent<StartGame>().width = (PlayerPrefs.HasKey("WidthC")) ? PlayerPrefs.GetInt("WidthC") : 5;
		}
		else if (GOParent == "Height")
		{
			gameObject.GetComponent<InputField>().text = (PlayerPrefs.HasKey("HeightC")) ? PlayerPrefs.GetInt("HeightC").ToString() : 5+"";
			startCustom.GetComponent<StartGame>().height = (PlayerPrefs.HasKey("HeightC")) ? PlayerPrefs.GetInt("HeightC") : 5;
		}else
		{
			gameObject.GetComponent<InputField>().text = (PlayerPrefs.HasKey("MinesAmountC")) ? PlayerPrefs.GetInt("MinesAmountC").ToString() : 1+"";
			startCustom.GetComponent<StartGame>().mines =  (PlayerPrefs.HasKey("MinesAmountC")) ? PlayerPrefs.GetInt("MinesAmountC") : 1;
		}
	}

	public void checkInputField()
	{
		if (gameObject.transform.parent.name == "Width" || gameObject.transform.parent.name == "Height")
		{
			int value = int.Parse(gameObject.GetComponent<InputField>().text);

			if (value < 5) value = 5;
			if (value > 60) value = 60;
			gameObject.GetComponent<InputField>().text = value.ToString();

			if (gameObject.transform.parent.name == "Width") 
				GameObject.Find("StartCustom").GetComponent<StartGame>().width = value;
			else
				GameObject.Find("StartCustom").GetComponent<StartGame>().height = value;
		}
		else
		{
			int value = int.Parse(gameObject.GetComponent<InputField>().text);
			int max = GameObject.Find("StartCustom").GetComponent<StartGame>().height * GameObject.Find("StartCustom").GetComponent<StartGame>().width - 1;
				
			if (value < 1) value = 1;
			if (value > max) value = max;

			gameObject.GetComponent<InputField>().text = value.ToString();

			GameObject.Find("StartCustom").GetComponent<StartGame>().mines = value;
		}
	}
}
