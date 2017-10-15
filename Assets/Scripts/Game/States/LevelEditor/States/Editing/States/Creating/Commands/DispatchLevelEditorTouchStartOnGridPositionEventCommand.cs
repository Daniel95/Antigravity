using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchStartOnGridPositionEventCommand : Command {

    [Inject] private LevelEditorTouchStartOnGridPositionEvent touchStartOnGridPositionEvent;

    [InjectParameter] private Vector2 touchDownScreenPosition;

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(touchDownScreenPosition);
        touchStartOnGridPositionEvent.Dispatch(gridPosition);
    }

}
