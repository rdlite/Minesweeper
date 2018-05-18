using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMeneController : MonoBehaviour {
    public float movingTime = 0;
    
    void Update () {
        movingTime += Time.deltaTime;
        transform.position += new Vector3(0.2f, 0.2f, 0);
        if (movingTime >= 200) { gameObject.transform.position = new Vector2(-1825, -1488); movingTime = 0; }
	}
}
