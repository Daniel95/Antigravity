using IoCPlus;
using UnityEngine;

public class LevelEditorResetSelectedLevelObjectVelocityCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        selectedLevelObjectRef.Get().Rigidbody.velocity = Vector2.zero;
    }

}
