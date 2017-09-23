using IoCPlus;

public class LevelEditorSpawnTileAtSwipePositionCommand : Command {

    [Inject] private Ref<ILevelEditorTiles> levelEditorTilesRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        levelEditorTilesRef.Get().SpawnTileAtScreenPosition(swipeMoveEventParameter.Position);
    }

}
