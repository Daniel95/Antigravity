using IoCPlus;
using UnityEngine;

public class AbortIfGameObjectIsNotATileCommand : Command {

    [InjectParameter] private GameObject gameObject;

    protected override void Execute() {
        if(!GenerateableTileLibrary.IsTile(gameObject.name)) {
            Abort();
        }
    }

}
