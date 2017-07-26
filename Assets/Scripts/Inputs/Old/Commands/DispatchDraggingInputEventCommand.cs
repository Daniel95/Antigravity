using IoCPlus;
using UnityEngine;

public class DispatchDraggingInputEventCommand : Command {

    [Inject] private DraggingInputEvent draggingInputEvent;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        draggingInputEvent.Dispatch(direction);
    }
}
