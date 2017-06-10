using IoCPlus;

public class StartScreenShakeCommand : Command {

    [Inject] private Ref<IScreenShake> screenShakeRef;

    protected override void Execute() {
        screenShakeRef.Get().StartScreenShake();
    }
}
