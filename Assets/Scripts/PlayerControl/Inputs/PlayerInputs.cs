using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour {

    private enum InputTypes { PC, Mobile };

    [SerializeField]
    private InputTypes inputTypeUsed;

    public Action<Vector2> dragging;

    public Action<Vector2> release;

    public Action cancelDrag;

    private MobileInputs mobileInputs;

    private PCInputs pcInputs;

    void Start() {

        mobileInputs = GetComponent<MobileInputs>();
        pcInputs = GetComponent<PCInputs>();

        //assign to the right controls, given by inputTypeUsed 
        if (inputTypeUsed == InputTypes.Mobile)
        {
            mobileInputs.StartUpdatingInputs();

            mobileInputs.dragging += dragging;
            mobileInputs.release += release;
            mobileInputs.cancelDrag += cancelDrag;
        }
        else if (inputTypeUsed == InputTypes.PC)
        {
            pcInputs.StartUpdatingInputs();

            pcInputs.dragging += dragging;
            pcInputs.release += release;
            pcInputs.cancelDrag += cancelDrag;
        }
    }

    public InputsBase GetInputController()
    {
        if(inputTypeUsed == InputTypes.Mobile)
        {
            return mobileInputs;
        }
        else
        {
            return pcInputs;
        }
    }
}
