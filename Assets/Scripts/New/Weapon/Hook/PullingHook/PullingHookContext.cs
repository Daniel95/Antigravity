using IoCPlus;

public class PullingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<HookProjectileIsAttachedEvent>()
            .Do<AbortIfHookAbleLayerIsNotHookedLayerCommand>(HookableLayers.PullSurface)
            .Do<PullingHookPullCommand>()
            .Dispatch<CancelHookEvent>()
            .OnAbort<DispatchCancelHookEventCommand>();
    }
}

