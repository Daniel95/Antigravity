using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarter : MonoBehaviour {

    private CharScriptAccess charAccess;
    private PlayerActivateWeapon playerActivateWeapon;
    private PlayerInputs playerInputs;

    private void Awake()
    {
        charAccess = GetComponent<CharScriptAccess>();
        playerInputs = GetComponent<PlayerInputs>();
        playerActivateWeapon = GetComponent<PlayerActivateWeapon>();
    }

    private void Start()
    {
        //activate the reverse input for the player
        //only gets fired if the inputs systems delegate is active too
        playerInputs.reverse += charAccess.speedMultiplier.SmoothFlipMultiplier;

        playerInputs.SetHoldInput(true);

        GetComponent<PlayerActivateSlowTime>().SetSlowTimeInput(true);
    }

    public void StartStandardPlayerInputs()
    {
        SetPlayerMovement(true);
        SetPlayerShootInputs(true);
        SetPlayerActionInput(true);
        SetPlayerReverseInput(true);
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
        playerInputs.SetReverseInput(_input);
    }

    public void SetPlayerShootInputs(bool _input)
    {
        playerInputs.SetShootInput(_input);
        playerActivateWeapon.SetWeaponInput(_input);
    }
}
