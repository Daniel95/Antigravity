using UnityEngine;
using System;
using System.Collections;

public class GrapplingState : State, ITriggerer {

    [SerializeField]
    private GameObject gun;

    private GrapplingHook _grapplingHook;

    private CharScriptAccess _charAccess;

    private PlayerInputs _playerInputs;

    private LookAt _gunLookAt;

    private Coroutine _slingMovement;

    private Vector2 _lastVelocity;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    protected override void Awake()
    {
        base.Awake();

        _grapplingHook = GetComponent<GrapplingHook>();
        _charAccess = GetComponent<CharScriptAccess>();
        _playerInputs = GetComponent<PlayerInputs>();
        _gunLookAt = gun.GetComponent<LookAt>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //when we switch our speed, also switch the direction of our velocity
        _charAccess.SpeedMultiplier.SwitchedMultiplier += FakeSwitchSpeed;

        //activate a different direction movement when in this state
        _charAccess.ControlVelocity.StopDirectionalMovement();
        _slingMovement = StartCoroutine(SlingMovement());

        //exit the state when the grapple has released itself
        _grapplingHook.StoppedGrappleLocking += ExitGrapple;

        //subscribe to the space input, so we know when to exit our grapple
        _playerInputs.Jump += ExitGrapple;
    }

    IEnumerator SlingMovement()
    {
        while (true)
        {
            _charAccess.ControlVelocity.SetVelocity(_lastVelocity = _charAccess.ControlVelocity.GetVelocityDirection() * (_charAccess.ControlVelocity.CurrentSpeed * Mathf.Abs(_charAccess.ControlVelocity.SpeedMultiplier)));

            yield return new WaitForFixedUpdate();
        }
    }

    private void FakeSwitchSpeed() {
        _charAccess.ControlVelocity.SwitchVelocityDirection();
    }

    public override void Act()
    {
        base.Act();

        if (_charAccess.ControlVelocity.GetVelocity == Vector2.zero && _charAccess.ControlVelocity.CurrentSpeed != 0) {
            _charAccess.ControlDirection.ApplyLogicDirection(_charAccess.ControlVelocity.GetVelocityDirection(), _charAccess.CollisionDirection.GetCurrentCollDir());
            EnterOnFootState();
        }

        //update the rotation of the gun
        _gunLookAt.UpdateLookAt(_grapplingHook.Destination);
    }

    private void ExitGrapple() {

        _charAccess.ControlVelocity.SetDirection(_charAccess.ControlVelocity.GetVelocityDirection());

        if (StopTrigger != null)
            StopTrigger();

        _charAccess.SpeedMultiplier.MakeMultiplierPositive();

        EnterLaunchedState();
    }

    private void EnterLaunchedState() {
        StateMachine.ActivateState(StateID.LaunchedState);
    }

    private void EnterOnFootState() {

        StateMachine.ActivateState(StateID.OnFootState);
    }

    public override void ResetState()
    {
        base.ResetState();

        //unsubscripte from all relevant delegates
        _charAccess.SpeedMultiplier.SwitchedMultiplier -= FakeSwitchSpeed;
        _grapplingHook.StoppedGrappleLocking -= ExitGrapple;
        _playerInputs.Jump -= ExitGrapple;

        _grapplingHook.ExitGrappleLock();

        //return to the normal movement
        StopCoroutine(_slingMovement);
    }

    public override void OnCollEnter(Collision2D collision)
    {
        base.OnCollEnter(collision);

        _charAccess.SpeedMultiplier.MakeMultiplierPositive();

        if (_charAccess.ControlTakeOff.CheckToBounce(collision))
        {
            _charAccess.ControlTakeOff.Bounce(_lastVelocity.normalized, _charAccess.CollisionDirection.GetUpdatedCollDir(collision));
            EnterLaunchedState();
        }
        else
        {
            _charAccess.ControlDirection.ApplyLogicDirection(_lastVelocity.normalized, _charAccess.CollisionDirection.GetUpdatedCollDir(collision));
            EnterOnFootState();
        }
    }
}
