using IoCPlus;
using UnityEngine;

public class LevelEditorSpawnTileAtScreenPositionCommand : Command {

    [Inject] private Ref<ILevelEditorCreating> levelEditorCreatingRef;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        levelEditorCreatingRef.Get().SpawnTileAtScreenPosition(screenPosition);
    }

}
