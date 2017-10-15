using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchUpOnWorldEventCommand : Command {

    [Inject] private LevelEditorTouchUpOnWorldEvent touchUpOnWorldEvent;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        touchUpOnWorldEvent.Dispatch(worldPosition);
    }

}
