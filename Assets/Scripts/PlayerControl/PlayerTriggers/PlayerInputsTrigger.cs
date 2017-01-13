using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputsTrigger : MonoBehaviour, ITriggerable
{
    public bool triggered { get; set; }

    [SerializeField]
    private GameObject player;

    private PlayerStarter playerStarter;

    private PlayerInputs playerInputs;

    private enum InputType { All, Shoot, Action, Reverse, Hold };

    [SerializeField]
    private InputType[] inputsTypes;

    private void Awake()
    {
        playerStarter = player.GetComponent<PlayerStarter>();
        playerInputs = player.GetComponent<PlayerInputs>();
    }

    public void TriggerActivate()
    {
        for (int i = 0; i < inputsTypes.Length; i++)
        {
            SetInput(inputsTypes[i], true);
        }
    }

    public void TriggerStop()
    {
        for (int i = 0; i < inputsTypes.Length; i++)
        {
            SetInput(inputsTypes[i], false);
        }
    }

    private void SetInput(InputType _inputType, bool _inputState)
    {
        if(_inputType == InputType.All)
        {
            playerInputs.SetInputs(_inputState);
        }
        else if (_inputType == InputType.Shoot)
        {
            playerStarter.SetPlayerShootInputs(_inputState);
        }
        else if (_inputType == InputType.Action)
        {
            playerStarter.SetPlayerActionInput(_inputState);
        }
        else if (_inputType == InputType.Reverse)
        {
            playerStarter.SetPlayerReverseInput(_inputState);
        }
        else if (_inputType == InputType.Hold)
        {
            playerStarter.SetPlayerSlowTimeInput(_inputState);
        }
    }
}
