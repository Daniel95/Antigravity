using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputsTrigger : MonoBehaviour, ITriggerable
{
    public bool Triggered { get; set; }

    [SerializeField]
    private GameObject player;

    private PlayerStarter _playerStarter;

    private PlayerInputs _playerInputs;

    private enum InputType { All, Shoot, Action, Reverse, Hold };

    [SerializeField]
    private InputType[] inputsTypes;

    private void Awake()
    {
        _playerStarter = player.GetComponent<PlayerStarter>();
        _playerInputs = player.GetComponent<PlayerInputs>();
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
            _playerInputs.SetInputs(_inputState);
        }
        else if (_inputType == InputType.Shoot)
        {
            _playerStarter.SetPlayerShootInputs(_inputState);
        }
        else if (_inputType == InputType.Action)
        {
            _playerStarter.SetPlayerActionInput(_inputState);
        }
        else if (_inputType == InputType.Reverse)
        {
            _playerStarter.SetPlayerReverseInput(_inputState);
        }
        else if (_inputType == InputType.Hold)
        {
            _playerStarter.SetPlayerSlowTimeInput(_inputState);
        }
    }
}
