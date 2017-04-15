using IoCPlus;

public class StartSlowTimeCommand : Command {

    [Inject] private Ref<ISlowTime> slowTimeRef;

    protected override void Execute() {
        slowTimeRef.Get().StartSlowTime();
    }
}
