using IoCPlus;
using UnityEngine;

public class DispatchPlayerSetMoveDirectionEventCommand : Command {

    [Inject] private PlayerSetMoveDirectionEvent playerSetMoveDirectionEvent;

    [InjectParameter] private Vector2 moveDirection;

    protected override void Execute() {
        playerSetMoveDirectionEvent.Dispatch(moveDirection);
    }
}
