using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarter : MonoBehaviour {

    private CharScriptAccess _charAccess;
    private PlayerActivateWeapon _playerActivateWeapon;
    private PlayerInputs _playerInputs;
    private PlayerActivateSlowTime _playerActivateSlowTime;

    private void Awake()
    {
        _charAccess = GetComponent<CharScriptAccess>();
        _playerInputs = GetComponent<PlayerInputs>();
        _playerActivateWeapon = GetComponent<PlayerActivateWeapon>();
        _playerActivateSlowTime = GetComponent<PlayerActivateSlowTime>();
    }

    public void StartStandardPlayerInputs()
    {
        SetPlayerMovement(true);
        SetPlayerShootInputs(true);
        SetPlayerActionInput(true);
        SetPlayerReverseInput(true);
        SetPlayerSlowTimeInput(true);
    }

    public void StartMazeLvlPlayerInputs()
    {
        SetPlayerMovement(true);
        SetPlayerShootInputs(true);
        SetPlayerReverseInput(true);
        SetPlayerDoubleReverseInput(true);

        SetPlayerSlowTimeInput(true);
    }

    public void SetPlayerMovement(bool _move)
    {
        if(_move)
            _charAccess.ControlVelocity.StartDirectionalMovement();
        else
            _charAccess.ControlVelocity.StopDirectionalMovement();
    }

    public void SetPlayerActionInput(bool _input)
    {
        _playerInputs.SetActionInput(_input);
    }

    public void SetPlayerReverseInput(bool _input)
    {
        //activate the reverse input for the player
        _playerInputs.Reverse += _charAccess.SpeedMultiplier.SmoothFlipMultiplier;

        _playerInputs.SetReverseInput(_input);
    }

    private void SetPlayerDoubleReverseInput(bool _input)
    {
        _playerInputs.SetDoubleReverseInput(_input);
    }

    public void SetPlayerSlowTimeInput(bool _input)
    {
        _playerInputs.SetHoldInput(_input);

        _playerActivateSlowTime.SetSlowTimeInput(_input);
    }

    public void SetPlayerShootInputs(bool _input)
    {
        _playerInputs.SetShootInput(_input);
        _playerActivateWeapon.SetWeaponInput(_input);
    }
}
