using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {
	public int width, height, mines;

	void Start()
	{
		if(gameObject.transform.name != "Custom" && gameObject.transform.name != "Random" && gameObject.transform.name != "StartCustom") 
			gameObject.GetComponentInChildren<Text>().text = width + "\n" + height + "\n" + mines;
	}

    public void startGame()
    {
        if (gameObject.transform.name == "StartCustom") 
        {
            PlayerPrefs.SetInt("WidthC",	   width);
            PlayerPrefs.SetInt("HeightC",      height);
            PlayerPrefs.SetInt("MinesAmountC", mines);

            PlayerPrefs.SetString("isCustom", "true");
        }
        else 
        {
            PlayerPrefs.SetInt("Width",		  width);
            PlayerPrefs.SetInt("Height",      height);
            PlayerPrefs.SetInt("MinesAmount", mines);

            PlayerPrefs.SetString("isCustom", "false");
        }
        
        PlayerPrefs.SetString("GameType", gameObject.transform.name);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Minesweeper");
    }

	public void randomField()
	{
		int width = Random.Range(5, 50);
		int height = Random.Range(5, 50);

        PlayerPrefs.SetInt("Width",		  width);
        PlayerPrefs.SetInt("Height", 	  height);
        PlayerPrefs.SetInt("MinesAmount", Random.Range(1, width*height-1));

        UnityEngine.SceneManagement.SceneManager.LoadScene("Minesweeper");
	}
}