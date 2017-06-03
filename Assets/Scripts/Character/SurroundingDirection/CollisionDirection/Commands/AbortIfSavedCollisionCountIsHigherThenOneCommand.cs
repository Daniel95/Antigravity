using IoCPlus;
using UnityEngine;

public class AbortIfSavedCollisionCountIsHigherThenOneCommand : Command {

    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;

    protected override void Execute() {
        if(characterCollisionDirectionRef.Get().SavedCollisionsCount > 1) {
            Abort();
        }
    }
}
