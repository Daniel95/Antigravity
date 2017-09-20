using IoCPlus;

public class ResetCamerPositionCommand : Command {

    [Inject] private Ref<ICameraVelocity> cameraVelocityRef;

    protected override void Execute() {
        cameraVelocityRef.Get().ResetPosition();
    }

}
