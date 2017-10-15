using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchDownOnWorldEventCommand : Command {

    [Inject] private LevelEditorTouchDownOnWorldEvent touchDownOnWorldEvent;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        touchDownOnWorldEvent.Dispatch(worldPosition);
    }

}
