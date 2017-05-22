using IoCPlus;

public class CharacterBounceView : View, ICharacterBounce {

    [Inject] private Ref<ICharacterBounce> characterBounceRef;

    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;

    public override void Initialize() {
        characterBounceRef.Set(this);
    }

    public void Bounce(CharacterBounceEvent.Parameter characterBounceParameter) {
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
