using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorSwipeMovedOnWorldEvent : Command {

    [Inject] private LevelEditorSwipeMovedOnWorldEvent swipeMovedEvent;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMovedEventParameter;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(swipeMovedEventParameter.Position);
        swipeMovedEvent.Dispatch(worldPosition);
    }

}
