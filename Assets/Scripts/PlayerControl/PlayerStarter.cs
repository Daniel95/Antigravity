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

    public void SetPlayerMovement(bool move)
    {
        if (move)
            _charAccess.ControlVelocity.StartDirectionalMovement();
        else
            _charAccess.ControlVelocity.StopDirectionalMovement();
    }

    public void SetPlayerActionInput(bool input)
    {
        _playerInputs.SetActionInput(input);
    }

    public void SetPlayerReverseInput(bool input)
    {
        _playerInputs.SetReverseInput(input);
    }

    private void SetPlayerDoubleReverseInput(bool input)
    {
        _playerInputs.SetDoubleReverseInput(input);
    }

    public void SetPlayerSlowTimeInput(bool input)
    {
        _playerInputs.SetHoldInput(input);

        _playerActivateSlowTime.SetSlowTimeInput(input);
    }

    public void SetPlayerShootInputs(bool input)
    {
        _playerInputs.SetShootInput(input);
        _playerActivateWeapon.SetWeaponInput(input);
    }
}
