using UnityEngine;
using System.Collections;

public class RevivedState : State
{
    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private int startDirectionRayLength = 50;

    private PlayerScriptAccess plrAccess;

    private ActivateWeapon activateWeapon;

    private LookAt lookAt;

    private BulletTime bulletTime;

    private bool bulletTimeActive;

    protected override void Awake()
    {
        base.Awake();

        plrAccess = GetComponent<PlayerScriptAccess>();

        activateWeapon = GetComponent<ActivateWeapon>();
        lookAt = gun.GetComponent<LookAt>();
        bulletTime = GetComponent<BulletTime>();
    }

    public override void EnterState()
    {
        base.EnterState();

        plrAccess.controlDirection.CancelLogicDirection();

        //deactivate the weapon so we no longer shoot when we click/tab
        activateWeapon.enabled = false;

        plrAccess.playerInputs.InputController.dragging += Dragging;
        plrAccess.playerInputs.InputController.release += Release;

        plrAccess.controlVelocity.SetDirection(Vector2.zero);
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