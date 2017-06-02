using IoCPlus;
using UnityEngine;

public class CharacterUpdateCollisionDirectionCommand : Command {

    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirection;

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        characterCollisionDirection.Get().GetUpdatedCollisionDirection(collision);
    }
}
