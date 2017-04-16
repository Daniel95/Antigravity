using IoCPlus;
using UnityEngine;

public class CharacterTryJumpCommand : Command {

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    protected override void Execute() {
        characterJumpRef.Get().TryJump();
    }
}
