using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour {

    private RectTransform bar;

    // Use this for initialization
    void Start () {
        bar = transform as RectTransform;
    }
	
	// Update is called once per frame
	void Update () {
        bar.sizeDelta = new Vector2(80, bar.sizeDelta.y);
    }
}
