using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {
	Row[] rows;

	void Awake()
	{
		rows = new Row[30];
		createRows("Beginner");
	}

	public void createRows(string gameType)
	{
		deleteRows();		

		// for (int i = 1; i <= PlayerPrefs.GetInt(gameType); i++)
		// {
		// 	GameObject row = Instantiate(Resources.Load("Prefabs/row") as GameObject);
		// 	row.transform.SetParent(GameObject.Find("rows").transform);
		// 	row.transform.GetChild(0).GetComponent<Text>().text = i + "";
		// 	row.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString(gameType + "Name" + i);
		// 	row.transform.GetChild(2).GetComponent<Text>().text = PlayerPrefs.GetString(gameType + "Time" + i);
		// 	float x = float.Parse(row.transform.GetChild(2).GetComponent<Text>().text);
		// }

		for (int i = 1; i <= PlayerPrefs.GetInt(gameType); i++)
		{
			Row row = new Row();
			
			row.name = PlayerPrefs.GetString(gameType + "Name" + i);
			row.time = float.Parse(PlayerPrefs.GetString(gameType + "Time" + i));

			rows[i] = row;
		}

    	for (int i = 1; i <= PlayerPrefs.GetInt(gameType) - 1; i++)
    	{            
        	for (int j = 1; j <= PlayerPrefs.GetInt(gameType) - 1; j++)
        	{     
            	if (rows[j + 1].time < rows[j].time) 
            	{
                	rows[29] = rows[j + 1]; 
                	rows[j + 1] = rows[j]; 
                	rows[j] = rows[29];
            	}
        	}
    	}

		for (int i = 1; i <= PlayerPrefs.GetInt(gameType); i++)
		{
			GameObject row = Instantiate(Resources.Load("Prefabs/row") as GameObject);	
			row.transform.SetParent(GameObject.Find("rows").transform);
			row.transform.GetChild(0).GetComponent<Text>().text = i + "";
			row.transform.GetChild(1).GetComponent<Text>().text = rows[i].name;
			row.transform.GetChild(2).GetComponent<Text>().text = rows[i].time.ToString();
		}
	}

	void deleteRows()
	{
		GameObject[] rows = GameObject.FindGameObjectsWithTag("Row");
		foreach (GameObject row in rows)
			Destroy(row);
	}

	class Row
	{
		public string name;
		public float time;
	}
}
