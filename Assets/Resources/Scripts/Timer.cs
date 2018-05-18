using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public static float timer;

	public static bool stopTimer = false;

	GameObject[] numbers = new GameObject[3];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++)
			numbers[i] = transform.GetChild(i).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (FieldCreator.isTouched && !FieldCreator.askWindowIsOpen && !stopTimer)
			timer += Time.deltaTime;

		numbers[0].GetComponent<SpriteRenderer>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().numbers[(int)timer/100];
		numbers[1].GetComponent<SpriteRenderer>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().numbers[(int)timer/10%10];
		numbers[2].GetComponent<SpriteRenderer>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().numbers[(int)timer%10];

		if (timer > 999) timer = 999;
	}
}
