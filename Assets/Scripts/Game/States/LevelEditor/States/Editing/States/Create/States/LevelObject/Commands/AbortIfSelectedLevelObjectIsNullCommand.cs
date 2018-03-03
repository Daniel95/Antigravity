using IoCPlus;
using UnityEngine;

public class AbortIfSelectedLevelObjectIsNullCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        if ((Object)selectedLevelObjectRef.Get() == null) {
            Abort();
        }
    }

}
