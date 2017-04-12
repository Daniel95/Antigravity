using IoCPlus;
using UnityEngine;

public class CheckBounceCommand : Command {

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    protected override void Execute() {
        characterJumpRef.Get().CheckBounce();
    }
}
