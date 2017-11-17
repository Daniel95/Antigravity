using IoCPlus;
using UnityEngine;

public class LevelEditorAddRigidBodyToSelectedLevelObjectCommand : Command {

    protected override void Execute() {
        Rigidbody2D levelObjectRigidBody = LevelEditorSelectedLevelObjectStatus.LevelObject.AddComponent<Rigidbody2D>();
        levelObjectRigidBody.gravityScale = 0;
    }

}
