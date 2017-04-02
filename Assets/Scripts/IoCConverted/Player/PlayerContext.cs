using IoCPlus;

public class PlayerContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerModel>();

        Bind<CancelDragInputEvent>();
        Bind<DraggingInputEvent>();
        Bind<HoldingInputEvent>();
        Bind<JumpInputEvent>();
        Bind<ReleaseInputEvent>();
        Bind<ReleaseInDirectionInputEvent>();
        Bind<TappedExpiredInputEvent>();
        Bind<EnableActionInputEvent>();
        Bind<EnableShootingInputEvent>();

        On<EnterContextSignal>()
            .Do<InstantiatePlayerCommand>()
            .AddContext<WeaponContext>()
            .AddContext<InputContext>()
            .AddContext<PlayerStateContext>();
    }

}