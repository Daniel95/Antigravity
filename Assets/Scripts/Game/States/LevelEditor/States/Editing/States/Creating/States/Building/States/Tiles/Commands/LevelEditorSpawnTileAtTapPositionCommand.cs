using HedgehogTeam.EasyTouch;
using IoCPlus;

public class LevelEditorSpawnTileAtTapPositionCommand : Command {

    [Inject] private Ref<ILevelEditorTiles> levelEditorTilesRef;

    [InjectParameter] private Gesture gesture;

    protected override void Execute() {
        levelEditorTilesRef.Get().SpawnTileAtScreenPosition(gesture.position);
    }

}
