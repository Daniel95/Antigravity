using UnityEngine;
using System;
using System.Collections;

public class PlayerInputs : MonoBehaviour {

    private enum InputTypes { PC, Mobile };

    [SerializeField]
    private InputTypes inputTypeUsed;

    private Func<bool> jumpInput;

    private Func<Vector2> shootInput;

    void Start() {
        PCInputs pcInputs = GetComponent<PCInputs>();
        MobileInputs mobileInputs = GetComponent<MobileInputs>();

        if (inputTypeUsed == InputTypes.Mobile)
        {
            jumpInput += mobileInputs.JumpInput;
            shootInput += mobileInputs.ShootInput;
        }
        else if (inputTypeUsed == InputTypes.PC)
        {
            jumpInput += pcInputs.JumpInput;
            shootInput += pcInputs.ShootInput;
        }
    }

    public bool CheckJumpInput()
    {
        return jumpInput();
    }

    public Vector2 CheckShootInput()
    {
        return shootInput();
    }
}
