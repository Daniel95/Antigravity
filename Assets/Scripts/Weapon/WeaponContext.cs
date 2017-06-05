using IoCPlus;

public class WeaponContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<HookContext>();

        On<DraggingInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<SlowTimeCommand>()
            .Do<CharacterPointToDirectionCommand>()
            .Do<CharacterUpdateAimLineDestinationCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<WeaponSetShootDirectionCommand>()
            .Do<CharacterPointToShootDirectionCommand>()
            .Do<DispatchFireWeaponEventCommand>();

        On<CancelDragInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<CharacterStopAimLineCommand>()
            .Dispatch<CancelAimWeaponEvent>();

        On<ReleaseInputEvent>()
            .Do<StopSlowTimeCommand>()
            .Do<CharacterStopAimLineCommand>();

        On<HoldingInputEvent>()
            .Do<SlowTimeCommand>();
    }
}