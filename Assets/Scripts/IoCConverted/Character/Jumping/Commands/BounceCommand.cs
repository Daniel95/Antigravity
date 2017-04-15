using IoCPlus;
using UnityEngine;

public class BounceCommand : Command {

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    [InjectParameter] private DirectionParameter directionInfo;

    protected override void Execute() {
        characterJumpRef.Get().Bounce(directionInfo);
    }
}
