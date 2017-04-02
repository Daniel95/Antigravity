using UnityEngine;
using System.Collections;
using IoCPlus;

public class SlidingStateView : View {

    private CharScriptAccess _charAccess;
    private GrapplingHook _grapplingHook;

    private void Awake()
    {
        _grapplingHook = GetComponent<GrapplingHook>();
    }

    private void Start()
    {
        _charAccess = GetComponent<CharScriptAccess>();
    }

    public override void Initialize() {
        base.Initialize();

        _charAccess.ControlVelocity.StartDirectionalMovement();
    }

    public override void Dispose() {
        base.Dispose();

        Destroy(true);
    }

    public void Jump()
    {
        _charAccess.ControlTakeOff.TryJump();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (_charAccess.ControlTakeOff.CheckToBounce(collision)) {
            _charAccess.ControlTakeOff.Bounce(_charAccess.ControlVelocity.GetDirection(), _charAccess.CollisionDirection.GetUpdatedCollDir(collision));
        } else {
            _charAccess.ControlDirection.ApplyLogicDirection(_charAccess.ControlVelocity.GetDirection(), _charAccess.CollisionDirection.GetUpdatedCollDir(collision));
        }
    }
}
