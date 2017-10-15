using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchStartOnOnGridLevelObjectEventCommand : Command {

    [Inject] private LevelEditorTouchStartOnOnGridLevelObjectEvent touchStartOnOnGridLevelObjectEvent;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        touchStartOnOnGridLevelObjectEvent.Dispatch(gridPosition);
    }

}
