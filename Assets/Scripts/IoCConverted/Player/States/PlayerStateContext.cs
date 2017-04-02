using IoCPlus;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerModel>();

        On<EnterContextSignal>()
            .AddContext<FloatingStateContext>()
            .AddContext<GrapplingStateContext>()
            .AddContext<SlidingStateContext>()
            .Do<InstantiatePlayerViewCommand<FloatingStateView>>();
            

        On<ActivateFloatingStateEvent>()
            .GotoState<FloatingStateContext>();

        On<GrapplingHookStartedEvent>()
            .GotoState<GrapplingStateContext>();
            
    }

}