using IoCPlus;
using UnityEngine;

public class DispatchPlayerCollisionWithNewDirectionEventCommand : Command {

    [Inject] private PlayerCollisionWithNewDirectionEvent playerCollisionWithNewDirectionEvent;

    [InjectParameter] private Collision2D collision;
    [InjectParameter] private Vector2 collisionDirection;

    protected override void Execute() {
        playerCollisionWithNewDirectionEvent.Dispatch(collision, collisionDirection);
    }

}
