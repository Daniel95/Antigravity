using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDefaultInputType : MonoBehaviour {

    [SerializeField]
    private PlayerInputs playerInputs;

    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();

        //check the inputType of our platform in the playerInputs scripts, if that is equal to JoyStick InputType our toggle is on, else it is off
        toggle.isOn = playerInputs.GetPlatformDefaultInputType() == PlayerInputs.InputType.JoyStick;
    }

}
