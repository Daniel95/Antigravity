using UnityEngine;
using System.Collections;

public class SwitchingGravityState : State
{

    [SerializeField]
    private KeyCode switchGravityKey = KeyCode.Space;

    private ControlGravity playerGravity;

    protected override void Awake()
    {
        base.Awake();
        playerGravity = GetComponent<ControlGravity>();
    }

    public override void Act()
    {
        base.Act();
        if (Input.GetKeyDown(switchGravityKey))
        {
            playerGravity.SwitchGravity();
        }
    }

    public void ExitState()
    {
        stateMachine.DeactivateState(StateID.SwitchingGravityState);
    }
}