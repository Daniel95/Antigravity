using IoCPlus;

public class StopGrapplingMidAirCommand : Command {

    [Inject] private Ref<IGrapplingState> grapplingStateRef;

    protected override void Execute() {
        grapplingStateRef.Get().StopGrapplingMidAir();
    }
}
