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

    public void StartStandardPlayerInputs()
    {
        StartPlayerMovement();
        StartPlayerShootInputs();
        StartPlayerKeyInputs();

        //activate the reverse input for the player
        playerInputs.InputController.flipSpeed += charAccess.speedMultiplier.SmoothFlipMultiplier;
    }

    public void StartPlayerMovement()
    {
        charAccess.controlVelocity.StartDirectionalMovement();
    }

    public void StartPlayerShootInputs()
    {
        playerInputs.StartShootInputs();

        playerActivateWeapon.StartWeaponInput();
    } 

    public void StartPlayerKeyInputs()
    {
        playerInputs.StartKeyInputs();
    }
}
