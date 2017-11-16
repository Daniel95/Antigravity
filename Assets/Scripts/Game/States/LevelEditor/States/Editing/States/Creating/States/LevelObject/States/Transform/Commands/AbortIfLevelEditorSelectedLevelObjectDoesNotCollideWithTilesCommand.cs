using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectDoesNotCollideWithTilesCommand : Command {

    protected override void Execute() {
        bool collidingWithTile = LevelEditorSelectedLevelObjectStatus.CollisionColliders.Exists(x => GenerateableTileLibrary.IsTile(x.name));
        if (!collidingWithTile) {
            Abort();
        }
    }


}
