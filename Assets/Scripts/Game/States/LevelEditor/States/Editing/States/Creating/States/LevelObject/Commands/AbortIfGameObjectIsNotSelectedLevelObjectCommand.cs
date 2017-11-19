using IoCPlus;
using UnityEngine;

public class AbortIfGameObjectIsNotSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    [InjectParameter] private GameObject gameObject; 

    protected override void Execute() {
        if(gameObject != selectedLevelObjectRef.Get().GameObject) {
            Abort();
        }
        
    }

}
