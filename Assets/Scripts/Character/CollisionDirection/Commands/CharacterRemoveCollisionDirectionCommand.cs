using IoCPlus;
using UnityEngine;

public class CharacterRemoveCollisionDirectionCommand : Command {

    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;

    [InjectParameter] private Vector2 collisionDirection;

    protected override void Execute() {
        characterCollisionDirectionRef.Get().RemoveCollisionDirection(collisionDirection);
    }
}
