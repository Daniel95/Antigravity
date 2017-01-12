using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateButtonInput : MonoBehaviour {

    [SerializeField]
    private KeyCode keyInput;

    private Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
	}

    private void Update()
    {
        if(Input.GetKeyDown(keyInput))
        {
            button.onClick.Invoke();
        }
    }
}
