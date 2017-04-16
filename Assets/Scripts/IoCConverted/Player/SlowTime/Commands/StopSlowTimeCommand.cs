using IoCPlus;

public class StopSlowTimeCommand : Command {

    [Inject] private Ref<ISlowTime> slowTimeRef;

    protected override void Execute() {
        slowTimeRef.Get().StopSlowTime();
    }
}
