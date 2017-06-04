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
            .Do<DispatchFireWeaponEventCommand>();

        On<CancelDragInputEvent>()
            .Do<AbortIfGameIsPauzedCommand>()
            .Do<CharacterStopAimLineCommand>()
            .Dispatch<CancelAimWeaponEvent>();

        On<ReleaseInputEvent>()
            .Do<StopSlowTimeCommand>()
            .Do<CharacterStopAimLineCommand>();
    }
}