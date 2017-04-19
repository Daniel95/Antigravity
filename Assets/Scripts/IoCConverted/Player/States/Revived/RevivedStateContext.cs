using IoCPlus;

public class RevivedStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewOnPlayerCommand<RevivedStateView>>()
            .Do<CharacterResetVelocityCommand>()
            .Do<CharacterResetMoveDirectionCommand>()
            .Do<CancelDragWeaponInputCommand>()
            .Do<DispatchEnableShootingInputEventCommand>(false)
            .Do<CharacterResetCollisionDirectionCommand>();

        On<DraggingInputEvent>()
            .Do<CharacterUpdateAimLineDestinationCommand>()
            .Do<RevivedStateAimingCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<DispatchEnableShootingInputEventCommand>(true)
            .Do<CharacterStopAimLineCommand>()
            .Do<CharacterSetMoveDirectionCommand>()
            .Do<CharacterTemporarySpeedIncreaseCommand>()
            .Dispatch<ActivateFloatingStateEvent>();
    }
}