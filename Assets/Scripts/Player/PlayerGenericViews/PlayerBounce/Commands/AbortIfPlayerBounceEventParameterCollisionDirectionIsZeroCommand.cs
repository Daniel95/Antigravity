using IoCPlus;
using UnityEngine;

public class AbortIfPlayerBounceEventParameterCollisionDirectionIsZeroCommand : Command {

    [InjectParameter] private PlayerBounceEvent.Parameter playerBounceParameter;

    protected override void Execute() {
        if (playerBounceParameter.CollisionDirection == Vector2.zero) {
            Abort();
        }
    }
}
