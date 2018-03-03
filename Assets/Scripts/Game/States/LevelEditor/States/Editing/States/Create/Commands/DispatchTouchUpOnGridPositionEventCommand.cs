using IoCPlus;
using UnityEngine;

public class DispatchTouchUpOnGridPositionEventCommand : Command {

    [Inject] private TouchUpOnGridPositionEvent levelEditorTouchUpOnGridPositionEvent;

    [InjectParameter] private Vector2 touchDownScreenPosition;

    protected override void Execute() {
        float nodeSize = LevelEditorGridNodeSizeLibrary.Instance.NodeSize;
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(touchDownScreenPosition);
        levelEditorTouchUpOnGridPositionEvent.Dispatch(gridPosition);
    }

}
