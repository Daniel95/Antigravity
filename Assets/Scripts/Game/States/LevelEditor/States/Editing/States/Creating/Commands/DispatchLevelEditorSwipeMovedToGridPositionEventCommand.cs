using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorSwipeMovedToGridPositionEventCommand : Command {

    [Inject] private LevelEditorSwipeMovedToGridPositionEvent levelEditorSwipeMovedToGridPositionEvent;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(swipeMoveEventParameter.Position);
        levelEditorSwipeMovedToGridPositionEvent.Dispatch(gridPosition);
    }

}
