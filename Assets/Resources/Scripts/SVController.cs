using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SVController : MonoBehaviour {
	public Sprite ON, OFF;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey(gameObject.transform.name))	
			PlayerPrefs.SetString(gameObject.transform.name, "true");

		if (gameObject.transform.name == "Sound")
		{ 
			GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = (PlayerPrefs.GetString("Sound") == "true") ?
				1 : 0;
		
		}
	}

	void Update()
	{
		if (PlayerPrefs.GetString(gameObject.transform.name) == "true")
			gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ON;
		else gameObject.transform.GetChild(0).GetComponent<Image>().sprite = OFF;
	}

	public void changeValue()
	{
		if (PlayerPrefs.GetString(gameObject.transform.name) == "true")
		{
			PlayerPrefs.SetString(gameObject.transform.name, "false");

			if (gameObject.transform.name == "Sound") { GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0; }
		}
		else 
		{ 
			PlayerPrefs.SetString(gameObject.transform.name, "true");

			if (gameObject.transform.name == "Sound") { GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 1; }
			if (gameObject.transform.name == "Vibration") { Handheld.Vibrate(); }
		}
	}
}
