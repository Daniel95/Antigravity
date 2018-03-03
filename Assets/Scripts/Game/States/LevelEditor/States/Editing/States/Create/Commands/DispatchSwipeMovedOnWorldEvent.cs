using IoCPlus;
using UnityEngine;

public class DispatchSwipeMovedOnWorldEvent : Command {

    [Inject] private SwipeMovedOnWorldEvent swipeMovedEvent;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMovedEventParameter;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(swipeMovedEventParameter.Position);
        swipeMovedEvent.Dispatch(new SwipeMovedOnWorldEvent.Parameter {
            Position = worldPosition,
            Delta = swipeMovedEventParameter.DeltaPosition,
        });
    }

}
