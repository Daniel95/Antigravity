using IoCPlus;
using UnityEngine;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<FloatingStateContext>()
            .AddContext<GrapplingStateContext>()
            .AddContext<SlidingStateContext>()
            .Do<InstantiateViewOnPlayerCommand<FloatingStateView>>();

        On<ActivateFloatingStateEvent>()
            .GotoState<FloatingStateContext>();

        On<GrapplingHookStartedEvent>()
            .GotoState<GrapplingStateContext>();
            
    }

}