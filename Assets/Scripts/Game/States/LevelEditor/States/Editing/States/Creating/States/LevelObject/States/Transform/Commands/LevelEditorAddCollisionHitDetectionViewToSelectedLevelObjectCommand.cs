using IoCPlus;
using UnityEngine;

public class LevelEditorAddCollisionHitDetectionViewToSelectedLevelObjectCommand : Command {

    [Inject] IContext context;

    protected override void Execute() {
        GameObject selectedLevelObject = LevelEditorSelectedLevelObjectStatus.LevelObject;
        CollisionHitDetectionView collisionHitDetectionView = selectedLevelObject.AddComponent<CollisionHitDetectionView>();
        context.AddView(collisionHitDetectionView, false);
    }

}
