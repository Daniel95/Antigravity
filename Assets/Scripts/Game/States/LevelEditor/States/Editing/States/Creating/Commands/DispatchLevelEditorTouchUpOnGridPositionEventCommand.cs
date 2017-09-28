using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchUpOnGridPositionEventCommand : Command {

    [Inject] private LevelEditorTouchUpOnGridPositionEvent levelEditorTouchUpOnGridPositionEvent;

    [Inject] private Vector2 touchDownScreenPosition;

    protected override void Execute() {
        float nodeSize = LevelEditorGridNodeSize.Instance.NodeSize;
        Vector2 gridPosition = GridHelper.ScreenToGridPosition(touchDownScreenPosition, nodeSize);
        levelEditorTouchUpOnGridPositionEvent.Dispatch(gridPosition);
    }

}
