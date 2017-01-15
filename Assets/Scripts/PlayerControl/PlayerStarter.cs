using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarter : MonoBehaviour {

    private CharScriptAccess charAccess;
    private PlayerActivateWeapon playerActivateWeapon;
    private PlayerInputs playerInputs;
    private PlayerActivateSlowTime playerActivateSlowTime;

    private void Awake()
    {
        charAccess = GetComponent<CharScriptAccess>();
        playerInputs = GetComponent<PlayerInputs>();
        playerActivateWeapon = GetComponent<PlayerActivateWeapon>();
        playerActivateSlowTime = GetComponent<PlayerActivateSlowTime>();
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
            charAccess.controlVelocity.StartDirectionalMovement();
        else
            charAccess.controlVelocity.StopDirectionalMovement();
    }

    public void SetPlayerActionInput(bool _input)
    {
        playerInputs.SetActionInput(_input);
    }

    public void SetPlayerReverseInput(bool _input)
    {
        //activate the reverse input for the player
        playerInputs.reverse += charAccess.speedMultiplier.SmoothFlipMultiplier;

        playerInputs.SetReverseInput(_input);
    }

    private void SetPlayerDoubleReverseInput(bool _input)
    {
        playerInputs.SetDoubleReverseInput(_input);
    }

    public void SetPlayerSlowTimeInput(bool _input)
    {
        playerInputs.SetHoldInput(_input);

        playerActivateSlowTime.SetSlowTimeInput(_input);
    }

    public void SetPlayerShootInputs(bool _input)
    {
        playerInputs.SetShootInput(_input);
        playerActivateWeapon.SetWeaponInput(_input);
    }
}
