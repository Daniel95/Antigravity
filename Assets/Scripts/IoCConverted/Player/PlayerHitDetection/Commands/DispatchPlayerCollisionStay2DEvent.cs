using IoCPlus;
using UnityEngine;

public class DispatchPlayerCollisionStay2DEvent : Command {

    [Inject] private PlayerCollisionStay2DEvent playerCollisionStay2DEvent;

    private GameObject gameObject;
    private Collision2D collision;

    protected override void Execute() {
        playerCollisionStay2DEvent.Dispatch(gameObject, collision);
    }
}
