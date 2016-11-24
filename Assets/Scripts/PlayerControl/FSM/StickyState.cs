using UnityEngine;
using System.Collections;

public class StickyState : State {

    [SerializeField]
    private KeyCode exitStickyInput = KeyCode.Space;

    [SerializeField]
    private float stickyGravity = 0.5f;

    private CharRaycasting charRaycasting;
    private ControlVelocity playerMovement;
    private ControlGravity playerGravity;

    protected override void Awake()
    {
        base.Awake();
        charRaycasting = GetComponent<CharRaycasting>();
        playerMovement = GetComponent<ControlVelocity>();
        playerGravity = GetComponent<ControlGravity>();
    }

    public override void EnterState()
    {
        base.EnterState();
        playerGravity.SetGravity(stickyGravity);
    }

    public override void Act()
    {
        base.Act();
        if (Input.GetKeyDown(exitStickyInput))
        {
            ExitState();
        }
    }

    private void ExitState()
    {
        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.DeactivateState(StateID.StickyState);
    }

    public override void OnCollExit2D(Collision2D coll) {
        if (coll.transform.CompareTag(Tags.StickyWall)) {
            ExitState();
        }
    }
}