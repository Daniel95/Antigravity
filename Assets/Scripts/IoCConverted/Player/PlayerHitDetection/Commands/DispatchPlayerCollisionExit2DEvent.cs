using IoCPlus;
using UnityEngine;

public class DispatchPlayerCollisionExit2DEvent : Command {

    [Inject] private PlayerCollisionExit2DEvent playerCollisionExit2DEvent;

    private GameObject gameObject;
    private Collision2D collision;

    protected override void Execute() {
        playerCollisionExit2DEvent.Dispatch(gameObject, collision);
    }
}
