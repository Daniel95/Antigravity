using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivateWeapon : MonoBehaviour {

    private ActivateWeapon _activateWeapon;
    private PlayerInputs _playerInputs;

    private void Start()
    {
        _activateWeapon = GetComponent<ActivateWeapon>();
        _playerInputs = GetComponent<PlayerInputs>();
    }

    public void SetWeaponInput(bool _input)
    {
        if(_input)
        {
            _playerInputs.Dragging += _activateWeapon.Aiming;
            _playerInputs.CancelDrag += _activateWeapon.CancelAiming;
            _playerInputs.ReleaseInDir += _activateWeapon.Shoot;
        }
        else
        {
            _playerInputs.Dragging -= _activateWeapon.Aiming;
            _playerInputs.CancelDrag -= _activateWeapon.CancelAiming;
            _playerInputs.ReleaseInDir -= _activateWeapon.Shoot;
        }
    }
}
