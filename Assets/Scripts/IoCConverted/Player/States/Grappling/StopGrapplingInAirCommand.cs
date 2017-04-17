using IoCPlus;

public class StopGrapplingInAirCommand : Command {

    [Inject] private Ref<IGrapplingState> grapplingStateRef;

    protected override void Execute() {
        grapplingStateRef.Get().StopGrapplingMidAir();
    }
}
