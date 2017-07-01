using IoCPlus;

public class ShakeInOutCommand : Command<ShakeType> {

    [Inject] private Ref<IScreenShake> screenShakeRef;

    protected override void Execute(ShakeType shakeType) {
        screenShakeRef.Get().ShakeInOut(shakeType);
    }

}
