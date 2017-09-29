using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchUpOnGridPositionEventCommand : Command {

    [Inject] private LevelEditorTouchUpOnGridPositionEvent levelEditorTouchUpOnGridPositionEvent;

    [InjectParameter] private Vector2 touchDownScreenPosition;

    protected override void Execute() {
        float nodeSize = LevelEditorGridNodeSize.Instance.NodeSize;
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(touchDownScreenPosition);
        levelEditorTouchUpOnGridPositionEvent.Dispatch(gridPosition);
    }

}
