using IoCPlus;
using UnityEngine;

public class CharacterBounceCommand : Command {

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    [InjectParameter] private CharacterDirectionParameter directionInfo;

    protected override void Execute() {
        characterJumpRef.Get().Bounce(directionInfo);
    }
}
