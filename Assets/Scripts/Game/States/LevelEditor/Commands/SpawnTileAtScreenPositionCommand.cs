using IoCPlus;
using UnityEngine;

public class SpawnTileAtScreenPositionCommand : Command {

    [Inject] private Ref<ILevelEditorInput> levelEditorInputRef;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        levelEditorInputRef.Get().SpawnTileAtScreenPosition(screenPosition);
    }

}
