using IoCPlus;
using UnityEngine;

public class AbortIfPreviousSelectedLevelObjectIsNullCommand : Command {

    [Inject(Label.PreviousSelectedLevelObject)] private Ref<ILevelObject> previousSelectedLevelObjectRef;

    protected override void Execute() {
        if((Object)previousSelectedLevelObjectRef.Get() == null) {
            Abort();
        }
    }

}
