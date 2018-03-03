using IoCPlus;
using UnityEngine;

public class DispatchSwipeStartOnWorldEventCommand : Command {

    [Inject] private SwipeStartOnWorldEvent swipeStartEvent;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        swipeStartEvent.Dispatch(worldPosition);
    }

}
