using UnityEngine;
using System.Collections;

public class GrapplingState : State {

    [SerializeField]
    private KeyCode cancelGrappleKey = KeyCode.Tab;

    [SerializeField]
    private int maxFramesStuck = 40;

    private ControlVelocity playerVelocity;
    private ControlDirection playerDirection;
    private GrapplingHook grapplingHook;

    private Coroutine checkStuckTimer;

    [SerializeField]
    private GameObject gun;

    private LookAt gunLookAt;

    protected override void Awake()
    {
        base.Awake();
        playerVelocity = GetComponent<ControlVelocity>();
        playerDirection = GetComponent<ControlDirection>();
        grapplingHook = GetComponent<GrapplingHook>();
        gunLookAt = gun.GetComponent<LookAt>();
    }

    public override void EnterState()
    {
        base.EnterState();
        playerVelocity.StopPlayerMovement();
    }

    public override void Act()
    {
        base.Act();
        if (Input.GetKeyDown(cancelGrappleKey))
        {
            ExitState();
        }

        //if we are stuck because of the grapplehook, unlock the grapple
        if (playerVelocity.GetVelocity == Vector2.zero && checkStuckTimer == null) {
            checkStuckTimer = StartCoroutine(CheckStuckTimer());
        }

        gunLookAt.UpdateLookAt(grapplingHook.Destination);
    }

    IEnumerator CheckStuckTimer()
    {
        int framesStuckCounter = 0;

        while (framesStuckCounter < maxFramesStuck)
        {
            framesStuckCounter++;
            yield return new WaitForFixedUpdate();
        }

        if (playerVelocity.GetVelocity == Vector2.zero)
        {
            ExitState();
        }
    }

    private void ExitState()
    {
        grapplingHook.ExitGrappleLock();
        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.DeactivateState(StateID.GrapplingState);
    }

    public override void OnCollEnter2D(Collision2D coll)
    {
        playerDirection.CheckDirection(grapplingHook.Direction);
        ExitState();
    }
}
