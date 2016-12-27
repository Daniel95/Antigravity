using UnityEngine;
using System.Collections;

public class RevivedState : State
{
    [SerializeField]
    private int framesInActiveAfterRespawning = 20;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private int startDirectionRayLength = 50;

    private PlayerScriptAccess plrAccess;

    private ActivateWeapon activateWeapon;

    private LookAt lookAt;

    private BulletTime bulletTime;

    private bool bulletTimeActive;

    private Frames frames;

    protected override void Awake()
    {
        base.Awake();

        plrAccess = GetComponent<PlayerScriptAccess>();

        frames = GetComponent<Frames>();
        
        activateWeapon = GetComponent<ActivateWeapon>();
        lookAt = gun.GetComponent<LookAt>();
        bulletTime = GetComponent<BulletTime>();
    }

    public override void EnterState()
    {
        base.EnterState();

        plrAccess.controlDirection.CancelLogicDirection();

        //make sure to reset the current touched input. If we are busy shooting, we reset the values and release;
        plrAccess.playerInputs.InputController.ResetTouched();

        //deactivate the weapon so we no longer shoot when we click/tab
        activateWeapon.enabled = false;

        //set our direction to zero so we dont move
        plrAccess.controlVelocity.SetDirection(Vector2.zero);

        //wait a few frames so the player dont start moving immediatly if he panic clicked right after he respawned
        frames.ExecuteAfterDelay(framesInActiveAfterRespawning, SubscribeToAimInput);
    }

    private void SubscribeToAimInput()
    {
        plrAccess.playerInputs.InputController.dragging += Dragging;
        plrAccess.playerInputs.InputController.release += Release;
    }

    private void Dragging(Vector2 _dir)
    {
        if (!bulletTime.BulletTimeActive)
        {
            bulletTime.StartBulletTime();
        }

        bulletTime.SetRayDestination = (Vector2)transform.position + (_dir * startDirectionRayLength);

        lookAt.UpdateLookAt((Vector2)transform.position + _dir);
    }

    private void Release(Vector2 _dir)
    {
        activateWeapon.enabled = true;

        bulletTime.StopBulletTime();
        plrAccess.controlVelocity.SetDirection(_dir * plrAccess.controlVelocity.GetMultiplierDir());

        plrAccess.controlVelocity.TempSpeedIncrease();

        EnterLaunchedState();
    }

    private void EnterLaunchedState()
    {
        plrAccess.playerInputs.InputController.dragging -= Dragging;
        plrAccess.playerInputs.InputController.release -= Release;

        stateMachine.ActivateState(StateID.LaunchedState);
        stateMachine.DeactivateState(StateID.RevivedState);
    }
}