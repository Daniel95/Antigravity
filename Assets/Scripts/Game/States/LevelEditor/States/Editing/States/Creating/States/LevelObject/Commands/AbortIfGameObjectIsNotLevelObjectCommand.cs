using IoCPlus;
using UnityEngine;

public class AbortIfGameObjectIsNotLevelObjectCommand : Command {

    [InjectParameter] private GameObject gameObject;

    protected override void Execute() {
        if(!GenerateableLevelObjectLibrary.IsLevelObject(gameObject.name)) {
            Abort();
        }
    }

}
