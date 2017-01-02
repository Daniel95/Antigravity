using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour, ITriggerable {

    public enum InputType { Drag, JoyStick };

    [SerializeField]
    private InputType defaultInputTypePC = InputType.Drag;

    [SerializeField]
    private InputType defaultInputTypeMobile = InputType.JoyStick; 

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

    private void Start()
    {
        if (Platform.PlatformIsMobile())
        {
            inputTypeUsed = defaultInputTypeMobile;
        }
        else
        {
            inputTypeUsed = defaultInputTypePC;
        }
    }

    public void TriggerActivate()
    {
        StartShootInputs();
        StartKeyInputs();
    }

    public void TriggerStop()
    {
        StopShootInputs();
        StopKeyInputs();
    }

    public void StartShootInputs()
    {
        //assign to the right controls, given by inputTypeUsed 
        if (inputTypeUsed == InputType.Drag)
        {
            inputController.StartUpdatingDragInputs();
        }
        else
        {
            inputController.StartUpdatingJoyStickInputs();
        }
    }

    public void StopShootInputs()
    {
        //stop the right controls, given by inputTypeUsed 
        if (inputTypeUsed == InputType.Drag)
        {
            inputController.StopUpdatingDragInputs();
        }
        else
        {
            inputController.StopUpdatingJoyStickInputs();
        }
    }

    public void StartKeyInputs()
    {
        if (!Platform.PlatformIsMobile()) {
            pcInputs.StartUpdatingKeyInputs();
        }
    }

    public void StopKeyInputs()
    {
        if (!Platform.PlatformIsMobile())
        {
            pcInputs.StopUpdatingKeyInputs();
        }
    }

    //used by other scripts to assign themselfes to input delegates
    public InputsBase InputController
    {
        get { return inputController; }
    }

    private InputsBase GetInputController()
    {
        if (Platform.PlatformIsMobile())
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
        if (inputTypeUsed == InputType.Drag)
        {
            inputController.StopUpdatingDragInputs();
            inputController.StartUpdatingJoyStickInputs();
        }
        else
        {
            inputController.StopUpdatingJoyStickInputs();
            inputController.StartUpdatingDragInputs();
        }
    }

    public InputType GetPlatformDefaultInputType()
    {
        if (Platform.PlatformIsMobile())
        {
            return defaultInputTypeMobile;
        }
        else
        {
            return defaultInputTypePC;
        }
    }

    public void Action()
    {
        print("action");
        inputController.Action();
    }

    public void Reverse()
    {
        inputController.FlipSpeed();
    }
}
