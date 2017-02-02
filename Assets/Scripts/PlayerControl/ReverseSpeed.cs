using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSpeed : MonoBehaviour {

    private SpeedMultiplier _speedMultiplier;
    private PlayerCharges _playerCharges;

    private void Start()
    {
        _speedMultiplier = GetComponent<SpeedMultiplier>();

        _playerCharges = GetComponent<PlayerCharges>();

        //activate the reverse input for the player
        GetComponent<PlayerInputs>().Reverse += CheckReverseCharge;
    }

    private void CheckReverseCharge()
    {
        if (_playerCharges.UseCharge(PlayerCharges.ChargeAbleAction.ReverseSpeed))
            _speedMultiplier.SmoothFlipMultiplier();
    }
}
