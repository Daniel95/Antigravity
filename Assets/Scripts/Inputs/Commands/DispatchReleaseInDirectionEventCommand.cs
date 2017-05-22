using IoCPlus;
using UnityEngine;

public class DispatchReleaseInDirectionEventCommand : Command {

    [Inject] private ReleaseInDirectionInputEvent releaseInDirectionInputEvent;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        releaseInDirectionInputEvent.Dispatch(direction);
    }
}