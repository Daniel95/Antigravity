using IoCPlus;

public class DispatchPlayerBounceEventCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterSurroundingDirection> characterSurroundingsDirection;

    [Inject] private PlayerBounceEvent playerBounceEvent;

    protected override void Execute() {
        playerBounceEvent.Dispatch(new PlayerBounceEvent.Parameter(
            characterVelocityRef.Get().MoveDirection,
            characterSurroundingsDirection.Get().GetSurroundingsDirection()
        ));
    }
}
