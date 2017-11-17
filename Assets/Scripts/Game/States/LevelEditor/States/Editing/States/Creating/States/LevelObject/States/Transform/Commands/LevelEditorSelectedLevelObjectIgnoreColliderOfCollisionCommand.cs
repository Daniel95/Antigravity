using IoCPlus;
using UnityEngine;

public class LevelEditorSelectedLevelObjectIgnoreColliderOfCollisionCommand : Command {

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        Physics2D.IgnoreCollision(LevelEditorSelectedLevelObjectStatus.LevelObjectCollider, collision.collider);
    }

}
