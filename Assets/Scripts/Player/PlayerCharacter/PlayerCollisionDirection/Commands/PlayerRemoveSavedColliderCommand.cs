using IoCPlus;
using UnityEngine;

public class PlayerRemoveSavedColliderCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        playerCollisionDirectionRef.Get().RemoveSavedCollider(collider);
    }
}
