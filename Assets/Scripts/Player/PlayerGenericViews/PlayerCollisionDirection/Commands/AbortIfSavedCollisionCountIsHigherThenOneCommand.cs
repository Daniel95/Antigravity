using IoCPlus;
using UnityEngine;

public class AbortIfSavedCollisionCountIsHigherThenOneCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    protected override void Execute() {
        if(playerCollisionDirectionRef.Get().SavedCollisionsCount > 1) {
            Abort();
        }
    }
}
