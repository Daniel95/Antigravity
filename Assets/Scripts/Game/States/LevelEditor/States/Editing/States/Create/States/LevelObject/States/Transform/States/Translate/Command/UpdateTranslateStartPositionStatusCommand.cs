using IoCPlus;
using UnityEngine;

public class UpdateTranslateStartPositionStatusCommand : Command {

    [InjectParameter] private Vector2 worldPosition;

    protected override void Execute() {
        TranslateStartPositionStatus.StartWorldPosition = worldPosition;
    }

}
