using IoCPlus;
using UnityEngine;

public class DispatchSwipeMovedToNewGridPositionEventCommand : Command {

    [Inject] private SwipeMovedToNewGridPositionEvent levelEditorSwipeMovedToNewGridPositionEvent;

    [InjectParameter] private Vector2 newGridPosition;

    protected override void Execute() {
        levelEditorSwipeMovedToNewGridPositionEvent.Dispatch(newGridPosition);
    }

}
