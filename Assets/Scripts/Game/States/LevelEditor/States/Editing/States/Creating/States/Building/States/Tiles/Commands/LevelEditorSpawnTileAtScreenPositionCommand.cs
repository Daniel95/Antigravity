using IoCPlus;
using UnityEngine;

public class LevelEditorSpawnTileAtScreenPositionCommand : Command {

    [Inject] private Ref<ILevelEditorTiles> levelEditorTilesRef;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        levelEditorTilesRef.Get().SpawnTileAtScreenPosition(screenPosition);
    }

}
