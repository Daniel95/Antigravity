using IoCPlus;

public class GrapplingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<Ref<IGrapplingState>>();

        On<EnterContextSignal>()
            .Do<InstantiateViewOnPlayerCommand<GrapplingStateView>>();

        On<JumpInputEvent>()
            .Do<StopGrapplingMidAirCommand>();
    }
}