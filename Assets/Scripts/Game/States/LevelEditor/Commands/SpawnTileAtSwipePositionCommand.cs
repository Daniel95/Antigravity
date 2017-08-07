using IoCPlus;
using UnityEngine;

public class SpawnTileAtSwipePositionCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().SpawnTileAtScreenPosition(swipeMoveEventParameter.Position);
    }

}
