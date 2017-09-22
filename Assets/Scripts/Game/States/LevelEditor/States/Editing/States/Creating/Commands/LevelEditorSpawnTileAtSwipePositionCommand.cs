using IoCPlus;

public class LevelEditorSpawnTileAtSwipePositionCommand : Command {

    [Inject] private Ref<ILevelEditorCreating> levelEditorCreatingRef;

    [InjectParameter] private SwipeMovedEvent.Parameter swipeMoveEventParameter;

    protected override void Execute() {
        levelEditorCreatingRef.Get().SpawnTileAtScreenPosition(swipeMoveEventParameter.Position);
    }

}
