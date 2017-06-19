using IoCPlus;

public class DispatchPlayerBounceEventCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject] private Ref<ICharacterSurroundingDirection> characterSurroundingsDirection;

    [Inject] private PlayerBounceEvent playerBounceEvent;

    protected override void Execute() {
        playerBounceEvent.Dispatch(new PlayerBounceEvent.Parameter(
            playerVelocityRef.Get().MoveDirection,
            characterSurroundingsDirection.Get().GetSurroundingsDirection()
        ));
    }
}
