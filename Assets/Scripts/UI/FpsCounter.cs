using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour {

    private Text text;

    private float deltaTime;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        text.text = Mathf.Round(1 / deltaTime).ToString();
    }
}
