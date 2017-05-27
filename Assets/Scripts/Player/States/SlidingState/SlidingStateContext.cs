﻿using IoCPlus;

public class SlidingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<GrapplingHookStopGrappleLockCommand>()
            .Do<CharacterActivateDirectionalMovementCommand>();
    }

}