using IoCPlus;
using UnityEngine;

public class DispatchTouchDownOnWorldEventCommand : Command {

    [Inject] private TouchDownOnWorldEvent touchDownOnWorldEvent;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        touchDownOnWorldEvent.Dispatch(worldPosition);
    }

}
