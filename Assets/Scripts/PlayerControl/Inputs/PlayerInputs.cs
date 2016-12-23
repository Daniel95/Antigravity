using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour {

    private enum InputFormat { PC, Mobile };

    public enum InputType { Standard, JoyStick };

    [SerializeField]
    private InputFormat inputFormatUsed;

    [SerializeField]
    private InputType inputTypeUsed;

    private MobileInputs mobileInputs;

    private PCInputs pcInputs;

    private InputsBase inputController;

    void Awake()
    {
        mobileInputs = GetComponent<MobileInputs>();
        pcInputs = GetComponent<PCInputs>();
        inputController = GetInputController();
    }

    public void StartInputs()
    {
        //assign to the right controls, given by inputTypeUsed 
        if (inputTypeUsed == InputType.Standard)
        {
            inputController.StartUpdatingStandardInputs();
        }
        else
        {
            inputController.StartUpdatingJoyStickInputs();
        }
    }

    //used by other scripts to assign themselfes to input delegates
    public InputsBase InputController
    {
        get { return inputController; }
    }

    private InputsBase GetInputController()
    {
        if(inputFormatUsed == InputFormat.Mobile)
        {
            return mobileInputs;
        }
        else
        {
            return pcInputs;
        }
    }

    public void SwitchControlType()
    {
        if (inputTypeUsed == InputType.Standard)
        {
            inputController.StopUpdatingStandardInputs();
            inputController.StartUpdatingJoyStickInputs();
        }
        else
        {
            inputController.StopUpdatingJoyStickInputs();
            inputController.StartUpdatingStandardInputs();
        }
    }
}
