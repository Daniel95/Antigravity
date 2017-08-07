using IoCPlus;
using UnityEngine;

public class SpawnTileAtScreenPositionCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().SpawnTileAtScreenPosition(screenPosition);
    }

}
