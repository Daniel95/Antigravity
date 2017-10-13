using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchDownOnOnGridLevelObjectEventCommand : Command {

    [Inject] private LevelEditorTouchDownOnOnGridLevelObjectEvent touchDownOnOnGridLevelObjectEvent;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        touchDownOnOnGridLevelObjectEvent.Dispatch(gridPosition);
    }

}
