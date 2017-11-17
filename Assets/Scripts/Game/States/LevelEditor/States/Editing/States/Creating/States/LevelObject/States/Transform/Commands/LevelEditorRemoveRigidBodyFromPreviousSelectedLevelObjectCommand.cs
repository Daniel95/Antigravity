using IoCPlus;
using UnityEngine;

public class LevelEditorRemoveRigidBodyFromPreviousSelectedLevelObjectCommand : Command {

    protected override void Execute() {
        Rigidbody2D levelObjectRigidBody = LevelEditorSelectedLevelObjectStatus.PreviousLevelObject.GetComponent<Rigidbody2D>();
        Object.Destroy(levelObjectRigidBody);
    }

}
