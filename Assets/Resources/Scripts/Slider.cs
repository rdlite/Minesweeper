using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour {
	public float time;
	public bool toFill = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (toFill) time += Time.deltaTime;
		else time -= Time.deltaTime;

		gameObject.GetComponent<Image>().fillAmount = time;

		if (time >= 1f)  toFill = false;
		else if (time <= 0) toFill = true;
	}
}
