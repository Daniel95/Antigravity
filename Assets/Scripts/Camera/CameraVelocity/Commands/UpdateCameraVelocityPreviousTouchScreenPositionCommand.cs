using IoCPlus;
using UnityEngine;

public class UpdateCameraVelocityPreviousTouchScreenPositionCommand : Command {

    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    [InjectParameter] private Vector2 touchScreenPosition;

    protected override void Execute() {
        cameraVelocityRef.Get().UpdatePreviousTouchScreenPosition(touchScreenPosition);
    }
}
