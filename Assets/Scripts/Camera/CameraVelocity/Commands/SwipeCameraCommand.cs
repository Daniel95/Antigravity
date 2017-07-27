using IoCPlus;
using UnityEngine;

public class SwipeCameraCommand : Command {

    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMovedEventParameter;

    protected override void Execute() {
        cameraVelocityRef.Get().Swipe(swipeMovedEventParameter.Position);
    }

}

