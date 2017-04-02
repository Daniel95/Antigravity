using UnityEngine;
using System.Collections;
using IoCPlus;

public class FloatingStateView : View
{
    [Inject] private ActivateSlidingStateEvent activateSlidingStateEvent;

    private CharScriptAccess _charAccess;
    private GrapplingHook _grapplingHook;

    private FutureDirectionIndicator _directionIndicator;

    protected void Awake()
    {
        _directionIndicator = GetComponent<FutureDirectionIndicator>();
        _charAccess = GetComponent<CharScriptAccess>();
        _grapplingHook = GetComponent<GrapplingHook>();
    }

    public override void Initialize() {
        base.Initialize();

        _charAccess.ControlVelocity.StartDirectionalMovement();

        _directionIndicator.PointToCeilVelocityDir();
    }

    public override void Dispose() {
        base.Dispose();

        Destroy(true);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (_charAccess.ControlTakeOff.CheckToBounce(collision))
        {
            _charAccess.ControlTakeOff.Bounce(_charAccess.ControlVelocity.GetDirection(), _charAccess.CollisionDirection.GetUpdatedCollDir(collision));
        }
        else
        {
            _charAccess.ControlDirection.ApplyLogicDirection(_charAccess.ControlVelocity.GetDirection(), _charAccess.CollisionDirection.GetUpdatedCollDir(collision));

            activateSlidingStateEvent.Dispatch();
        }
    }
}