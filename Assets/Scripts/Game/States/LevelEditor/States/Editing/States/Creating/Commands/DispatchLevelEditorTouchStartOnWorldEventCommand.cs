using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchStartOnWorldEventCommand : Command {

    [Inject] private LevelEditorTouchStartOnWorldEvent touchStartOnWorldEvent;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        touchStartOnWorldEvent.Dispatch(worldPosition);
    }

}
