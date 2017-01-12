using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivateWeapon : MonoBehaviour {

    private ActivateWeapon activateWeapon;
    private PlayerInputs playerInputs;

    private void Start()
    {
        activateWeapon = GetComponent<ActivateWeapon>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    public void SetWeaponInput(bool _input)
    {
        if(_input)
        {
            playerInputs.dragging += activateWeapon.Aiming;
            playerInputs.cancelDrag += activateWeapon.CancelAiming;
            playerInputs.releaseInDir += activateWeapon.Shoot;
        }
        else
        {
            playerInputs.dragging -= activateWeapon.Aiming;
            playerInputs.cancelDrag -= activateWeapon.CancelAiming;
            playerInputs.releaseInDir -= activateWeapon.Shoot;
        }
    }
}
