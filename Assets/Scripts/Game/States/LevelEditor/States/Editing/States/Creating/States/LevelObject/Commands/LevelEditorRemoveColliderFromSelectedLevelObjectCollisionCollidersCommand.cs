using IoCPlus;
using UnityEngine;

public class LevelEditorRemoveColliderFromSelectedLevelObjectCollisionCollidersCommand : Command {

    [Optional] [InjectParameter] private Collider2D collider;
    [Optional] [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        Collider2D colliderToAdd = collider ?? collision.collider;
        LevelEditorSelectedLevelObjectStatus.CollisionColliders.Remove(colliderToAdd);
    }

}
