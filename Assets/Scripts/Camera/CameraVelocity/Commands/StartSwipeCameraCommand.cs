using IoCPlus;
using UnityEngine;

public class StartSwipeCameraCommand : Command {

    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    [InjectParameter] private Vector2 touchScreenStartPosition;

    protected override void Execute() {
        cameraVelocityRef.Get().StartSwipe(touchScreenStartPosition);
    }

}
