using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorSwipeMovedToGridPositionEventCommand : Command {

    [Inject] private LevelEditorSwipeMovedToGridPositionEvent levelEditorSwipeMovedToGridPositionEvent;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        float nodeSize = LevelEditorGridNodeSize.Instance.NodeSize;
        Vector2 gridPosition = GridHelper.ScreenToGridPosition(swipeMoveEventParameter.Position, nodeSize);
        levelEditorSwipeMovedToGridPositionEvent.Dispatch(gridPosition);
    }

}
