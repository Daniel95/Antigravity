using IoCPlus;
using UnityEngine;

public class PlayerInputsTriggerView : View, ITriggerable
{
    [Inject] private InputModel inputModel;

    [Inject] private ActivateInputPlatformEvent enableInputTypeEvent;

    public bool Triggered { get; set; }

    [SerializeField]
    private GameObject player;

    private enum InputType { All, Shoot, Action };

    [SerializeField]
    private InputType[] inputsTypes;

    public void TriggerActivate()
    {
        for (int i = 0; i < inputsTypes.Length; i++)
        {
            EnableInput(inputsTypes[i], true);
        }
    }

    public void TriggerStop()
    {
        for (int i = 0; i < inputsTypes.Length; i++)
        {
            EnableInput(inputsTypes[i], false);
        }
    }

    private void EnableInput(InputType inputType, bool enable)
    {
        if(inputType == InputType.All) {

            enableInputTypeEvent.Dispatch(enable);
            inputModel.shootingInputIsEnabled = enable;
            inputModel.actionInputIsEnabled = enabled;
        } else if (inputType == InputType.Shoot) {

            inputModel.shootingInputIsEnabled = enable;
        }
        else if (inputType == InputType.Action) {

            inputModel.actionInputIsEnabled = enabled;
        }
    }
}
