using IoCPlus;
using UnityEngine;

public class StartSelectionFieldAtPositionCommand : Command {

    [Inject] private Ref<ILevelEditorInput> levelEditorInputRef;

    [InjectParameter] private Vector2 position;

    protected override void Execute() {
        levelEditorInputRef.Get().StartSelectionField(position);
    }

}
