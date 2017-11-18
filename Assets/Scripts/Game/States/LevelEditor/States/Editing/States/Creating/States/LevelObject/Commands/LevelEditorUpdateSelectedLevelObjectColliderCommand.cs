using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectColliderCommand : Command {

    protected override void Execute() {
        Collider2D collider = LevelEditorSelectedLevelObjectStatus.LevelObject.GetComponent<Collider2D>();
        LevelEditorSelectedLevelObjectStatus.LevelObjectCollider = collider; 
    }

}
