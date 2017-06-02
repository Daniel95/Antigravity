using IoCPlus;
using UnityEngine;

public class AbortIfCollisionDirectionIsNotZeroCommand : Command {

    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirection;

    protected override void Execute() {
        if(characterCollisionDirection.Get().GetCurrentCollisionDirection() != Vector2.zero) {
            Abort();
        }
    }
}
