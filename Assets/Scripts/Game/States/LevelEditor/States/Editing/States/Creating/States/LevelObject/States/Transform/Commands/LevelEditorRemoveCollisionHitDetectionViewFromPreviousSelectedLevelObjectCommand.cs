using IoCPlus;
using UnityEngine;

public class LevelEditorRemoveCollisionHitDetectionViewFromPreviousSelectedLevelObjectCommand : Command {

    protected override void Execute() {
        GameObject previousSelectedLevelObject = LevelEditorSelectedLevelObjectStatus.PreviousLevelObject;
        CollisionHitDetectionView collisionHitDetectionView = previousSelectedLevelObject.GetComponent<CollisionHitDetectionView>();
        collisionHitDetectionView.Destroy(true);
    }

}
