using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour {

    private Text _text;

    private float _deltaTime;

	// Use this for initialization
	void Start () {
        _text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        _text.text = Mathf.Round(1 / _deltaTime).ToString();
    }
}
