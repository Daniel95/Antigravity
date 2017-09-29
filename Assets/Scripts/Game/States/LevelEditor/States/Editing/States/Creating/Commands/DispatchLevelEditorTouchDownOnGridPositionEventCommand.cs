using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchDownOnGridPositionEventCommand : Command {

    [Inject] private LevelEditorTouchDownOnGridPositionEvent levelEditorTouchDownOnGridPositionEvent;

    [InjectParameter] private Vector2 touchDownScreenPosition;

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(touchDownScreenPosition);
        levelEditorTouchDownOnGridPositionEvent.Dispatch(gridPosition);
    }

}
