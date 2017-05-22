using IoCPlus;
using UnityEngine;

public class CharacterResetCollisionDirectionCommand : Command {

    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;

    protected override void Execute() {
        characterCollisionDirectionRef.Get().ResetCollisionDirection();
    }
}
