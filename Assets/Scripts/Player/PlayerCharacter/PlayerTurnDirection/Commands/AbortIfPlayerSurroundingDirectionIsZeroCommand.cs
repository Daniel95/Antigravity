using IoCPlus;
using UnityEngine;

public class AbortIfPlayerSurroundingDirectionIsZeroCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    protected override void Execute() {
        Vector2 surroundingsDirection = SurroundingDirectionHelper.GetSurroundingsDirection(playerCollisionDirectionRef.Get(), playerRaycastDirectionRef.Get());
        if(surroundingsDirection == Vector2.zero) {
            Abort();
        }
    }
}
