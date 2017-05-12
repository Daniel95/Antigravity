using IoCPlus;

public class PauseTimeCommand : Command<bool> {

    [Inject] private Ref<ITime> timeRef;

    protected override void Execute(bool pause) {
        timeRef.Get().PauseTime(pause);
    }
}
