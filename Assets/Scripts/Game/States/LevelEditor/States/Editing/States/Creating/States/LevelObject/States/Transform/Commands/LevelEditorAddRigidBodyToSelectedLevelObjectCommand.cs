using IoCPlus;
using UnityEngine;

public class LevelEditorAddRigidBodyToSelectedLevelObjectCommand : Command {

    protected override void Execute() {
        Rigidbody2D levelObjectRigidBody = LevelEditorSelectedLevelObjectStatus.LevelObject.AddComponent<Rigidbody2D>();
        levelObjectRigidBody.gravityScale = 0;
        levelObjectRigidBody.interpolation = RigidbodyInterpolation2D.Interpolate;
        levelObjectRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        levelObjectRigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        LevelEditorSelectedLevelObjectStatus.LevelObjectRigidBody = levelObjectRigidBody;
    }

}
