using IoCPlus;
using UnityEngine;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .GotoState<FloatingStateContext>();

        On<ActivateFloatingStateEvent>()
            .GotoState<FloatingStateContext>();

        On<GrapplingHookStartedEvent>()
            .GotoState<GrapplingStateContext>();
            
    }

}