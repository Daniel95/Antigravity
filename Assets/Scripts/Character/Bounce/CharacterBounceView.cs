using IoCPlus;

public class CharacterBounceView : View, ICharacterBounce {

    [Inject] private PlayerSetMoveDirectionEvent characterSetMoveDirectionEvent;

    public void Bounce(PlayerBounceEvent.Parameter characterBounceParameter) {
        if (characterBounceParameter.CollisionDirection.x != 0 || characterBounceParameter.CollisionDirection.y != 0) {
            if (characterBounceParameter.CollisionDirection.x != 0) {
                characterBounceParameter.MoveDirection.x *= -1;
            }
            if (characterBounceParameter.CollisionDirection.y != 0) {
                characterBounceParameter.MoveDirection.y *= -1;
            }

            characterSetMoveDirectionEvent.Dispatch(characterBounceParameter.MoveDirection);
        }
    }
}
