using IoCPlus;
using UnityEngine;

public class DispatchTouchStartOnGridPositionEventCommand : Command {

    [Inject] private TouchStartOnGridPositionEvent touchStartOnGridPositionEvent;

    [InjectParameter] private Vector2 touchDownScreenPosition;

    protected override void Execute() {
        Vector2 gridPosition = LevelEditorGridHelper.ScreenToGridPosition(touchDownScreenPosition);
        touchStartOnGridPositionEvent.Dispatch(gridPosition);
    }

}
