using IoCPlus;
using UnityEngine;

public class Swipe2FingersCameraCommand : Command {

    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    [InjectParameter] private SwipeMoved2FingersEvent.Parameter swipe2FingersMovedEventParameter;

    protected override void Execute() {
        cameraVelocityRef.Get().Swipe(swipe2FingersMovedEventParameter.Position);
    }

}

