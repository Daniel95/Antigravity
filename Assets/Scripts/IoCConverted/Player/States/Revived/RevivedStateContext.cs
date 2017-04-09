﻿using IoCPlus;

public class RevivedStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewOnPlayerCommand<RevivedStateView>>();

        On<DraggingInputEvent>()
            .Do<RevivedStateAimingCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<RevivedStateLaunchCommand>()
            .Dispatch<ActivateFloatingStateEvent>();

    }
}