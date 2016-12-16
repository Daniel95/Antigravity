using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour {

    private enum InputFormat { PC, Mobile };

    public enum InputType { Standard, JoyStick };

    [SerializeField]
    private InputFormat inputFormatUsed;

    [SerializeField]
    private InputType inputTypeUsed;

    public Action<Vector2> dragging;

    public Action<Vector2> release;

    public Action cancelDrag;

    private MobileInputs mobileInputs;

    private PCInputs pcInputs;

    void Start() {

        mobileInputs = GetComponent<MobileInputs>();
        pcInputs = GetComponent<PCInputs>();

        //assign to the right controls, given by inputTypeUsed 
        if (inputFormatUsed == InputFormat.Mobile)
        {
            if (inputTypeUsed == InputType.Standard)
            {
                mobileInputs.StartUpdatingStandardInputs();
            }
            else
            {
                mobileInputs.StartUpdatingJoyStickInputs();
            }

            mobileInputs.dragging += dragging;
            mobileInputs.release += release;
            mobileInputs.cancelDrag += cancelDrag;
        }
        else if (inputFormatUsed == InputFormat.PC)
        {
            if (inputTypeUsed == InputType.Standard)
            {
                pcInputs.StartUpdatingStandardInputs();
            }
            else
            {
                pcInputs.StartUpdatingJoyStickInputs();
            }

            pcInputs.dragging += dragging;
            pcInputs.release += release;
            pcInputs.cancelDrag += cancelDrag;
        }
    }

    public InputsBase GetInputController()
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
        if (inputFormatUsed == InputFormat.Mobile)
        {
            if (inputTypeUsed == InputType.Standard)
            {
                mobileInputs.StopUpdatingStandardInputs();
                mobileInputs.StartUpdatingJoyStickInputs();
            }
            else
            {
                mobileInputs.StopUpdatingJoyStickInputs();
                mobileInputs.StartUpdatingStandardInputs();
            }
        }
        else
        {
            if (inputTypeUsed == InputType.Standard)
            {
                pcInputs.StopUpdatingStandardInputs();
                pcInputs.StartUpdatingJoyStickInputs();
            }
            else
            {
                pcInputs.StopUpdatingJoyStickInputs();
                pcInputs.StartUpdatingStandardInputs();
            }
        }
    }
}
