using IoCPlus;
using UnityEngine;

public class DispatchTouchStartOnWorldEventCommand : Command {

    [Inject] private TouchStartOnWorldEvent touchStartOnWorldEvent;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        touchStartOnWorldEvent.Dispatch(worldPosition);
    }

}
