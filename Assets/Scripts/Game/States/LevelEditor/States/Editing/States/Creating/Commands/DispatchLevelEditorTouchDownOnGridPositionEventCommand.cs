using UnityEngine;
using System.Collections;
using IoCPlus;

public class DispatchLevelEditorTouchDownOnGridPositionEventCommand : Command {

    [Inject] private LevelEditorTouchDownOnGridPositionEvent levelEditorTouchDownOnGridPositionEvent;

    [Inject] private Vector2 touchDownScreenPosition;

    protected override void Execute() {
        float nodeSize = LevelEditorGridNodeSize.Instance.NodeSize;
        Vector2 gridPosition = GridHelper.ScreenToGridPosition(touchDownScreenPosition, nodeSize);
        levelEditorTouchDownOnGridPositionEvent.Dispatch(gridPosition);
    }

}
