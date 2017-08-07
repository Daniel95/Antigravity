using IoCPlus;
using UnityEngine;

public class StartSelectionFieldAtPositionCommand : Command {

    [Inject] private Ref<ILevelEditorBuildingInput> levelEditorInputRef;

    [InjectParameter] private Vector2 position;

    protected override void Execute() {
        levelEditorInputRef.Get().StartSelectionField(position);
    }

}
