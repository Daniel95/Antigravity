using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorSwipeStartOnWorldEventCommand : Command {

    [Inject] private LevelEditorSwipeStartOnWorldEvent swipeStartEvent;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        swipeStartEvent.Dispatch(worldPosition);
    }

}
