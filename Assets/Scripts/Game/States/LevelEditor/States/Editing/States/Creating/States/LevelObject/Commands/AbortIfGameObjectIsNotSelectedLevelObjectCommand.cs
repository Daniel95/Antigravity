using IoCPlus;
using UnityEngine;

public class AbortIfGameObjectIsNotSelectedLevelObjectCommand : Command {

    [InjectParameter] private GameObject gameObject; 

    protected override void Execute() {
        if(gameObject != LevelEditorSelectedLevelObjectStatus.LevelObject) {
            Abort();
        }
        
    }

}
