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

    public void StartWeaponInput()
    {
        playerInputs.InputController.dragging += activateWeapon.Dragging;
        playerInputs.InputController.cancelDrag += activateWeapon.CancelDrag;
        playerInputs.InputController.release += activateWeapon.Release;
    }

    public void StopWeaponInput()
    {
        playerInputs.InputController.dragging -= activateWeapon.Dragging;
        playerInputs.InputController.cancelDrag -= activateWeapon.CancelDrag;
        playerInputs.InputController.release -= activateWeapon.Release;
    }
}
