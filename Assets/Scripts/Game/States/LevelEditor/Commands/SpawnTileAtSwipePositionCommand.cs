using IoCPlus;
using UnityEngine;

public class SpawnTileAtSwipePositionCommand : Command {

    [Inject] private Ref<ILevelEditorBuildingInput> levelEditorInputRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        levelEditorInputRef.Get().SpawnTileAtScreenPosition(swipeMoveEventParameter.Position);
    }

}
