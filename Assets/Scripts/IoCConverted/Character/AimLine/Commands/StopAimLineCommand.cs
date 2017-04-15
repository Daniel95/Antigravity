using IoCPlus;

public class StopAimLineCommand : Command {

    [Inject] private Ref<IAimLine> aimLineRef;

    protected override void Execute() {
        aimLineRef.Get().StopAimLine();
    }
}
