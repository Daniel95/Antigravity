using IoCPlus;

public class AbortIfGameIsPauzedCommand : Command {

    [Inject] private Ref<ITime> timeRef;

	protected override void Execute() {
        if (timeRef.Get().IsPaused) {
            Abort();
        }
    }

}