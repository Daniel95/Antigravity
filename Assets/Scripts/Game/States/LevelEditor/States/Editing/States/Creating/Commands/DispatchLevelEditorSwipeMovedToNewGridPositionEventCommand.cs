using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorSwipeMovedToNewGridPositionEventCommand : Command {

    [Inject] private LevelEditorSwipeMovedToNewGridPositionEvent levelEditorSwipeMovedToNewGridPositionEvent;

    [InjectParameter] private Vector2 newGridPosition;

    protected override void Execute() {
        levelEditorSwipeMovedToNewGridPositionEvent.Dispatch(newGridPosition);
    }

}
