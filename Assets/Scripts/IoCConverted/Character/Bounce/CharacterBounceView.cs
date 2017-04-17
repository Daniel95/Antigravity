using IoCPlus;

public class CharacterBounceView : View, ICharacterBounce {

    [Inject] private Ref<ICharacterBounce> characterBounceRef;

    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;

    public override void Initialize() {
        characterBounceRef.Set(this);
    }

    public void Bounce(CharacterBounceParameter directionParameter) {
        if (directionParameter.CollisionDirection.x != 0 || directionParameter.CollisionDirection.y != 0) {
            if (directionParameter.CollisionDirection.x != 0) {
                directionParameter.MoveDirection.x *= -1;
            }
            if (directionParameter.CollisionDirection.y != 0) {
                directionParameter.MoveDirection.y *= -1;
            }

            characterSetMoveDirectionEvent.Dispatch(directionParameter.MoveDirection);
        }
    }
}
