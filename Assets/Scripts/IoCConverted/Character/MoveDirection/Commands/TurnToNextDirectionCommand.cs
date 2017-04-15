using IoCPlus;
using UnityEngine;

public class TurnToNextDirectionCommand : Command {

    [Inject] private Ref<ICharacterMoveDirection> characterMoveDirectionRef;

    [Inject] private DirectionParameter directionInfo;

    protected override void Execute() {
        characterMoveDirectionRef.Get().TurnToNextDirection(directionInfo);
    }
}
