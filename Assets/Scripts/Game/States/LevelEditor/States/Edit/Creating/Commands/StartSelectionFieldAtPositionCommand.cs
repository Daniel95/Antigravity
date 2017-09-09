using IoCPlus;
using UnityEngine;

public class StartSelectionFieldAtPositionCommand : Command {

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    [InjectParameter] private Vector2 position;

    protected override void Execute() {
        levelEditorCreatingInputRef.Get().StartSelectionField(position);
    }

}
