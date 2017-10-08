using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchDownOnLevelObjectEventCommand : Command {

    [Inject] private LevelEditorTouchDownOnLevelObjectEvent touchDownOnLevelObjectEvent;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        touchDownOnLevelObjectEvent.Dispatch(gridPosition);
    }

}
