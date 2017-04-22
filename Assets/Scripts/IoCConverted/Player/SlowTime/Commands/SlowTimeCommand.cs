using IoCPlus;

public class SlowTimeCommand : Command {

    [Inject] private Ref<ISlowTime> slowTimeRef;

    protected override void Execute() {
        slowTimeRef.Get().StartSlowTime();
    }
}
