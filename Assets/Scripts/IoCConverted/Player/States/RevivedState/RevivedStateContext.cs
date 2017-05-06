using IoCPlus;

public class RevivedStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewOnPlayerCommand<RevivedStateView>>()
            .Do<SetRevivedStateIsInPositionCommand>(false)
            .Do<SetRevivedStateIsReadyForLaunchInput>(false)
            .Do<CharacterResetVelocityCommand>()
            .Do<CharacterResetMoveDirectionCommand>()
            .Dispatch<CancelDragInputEvent>()
            .Do<DispatchEnableShootingInputEventCommand>(false)
            .Do<CharacterResetCollisionDirectionCommand>()
            .Do<StartRevivedStateDelayLaunchInputCommand>()
            .Do<MoveTowardsCheckpointCommand>();

        On<DraggingInputEvent>()
            .Do<CharacterUpdateAimLineDestinationCommand>()
            .Do<CharacterPointToDirectionCommand>();

        On<ReleaseInDirectionInputEvent>()
            .Do<AbortIfRevivedStateIsInPositionIsFalseCommand>()
            .Do<AbortIfRevivedStateIsReadyForLaunchInputIsFalseCommand>()
            .Do<DispatchEnableShootingInputEventCommand>(true)
            .Do<CharacterStopAimLineCommand>()
            .Do<CharacterSetMoveDirectionCommand>()
            .Do<CharacterTemporarySpeedIncreaseCommand>()
            .Dispatch<ActivateFloatingStateEvent>();

        On<ReachedMoveTowardsDestinationEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<SetRevivedStateIsInPositionCommand>(true);
    }
}