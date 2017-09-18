using IoCPlus;
using UnityEngine;

public class LevelEditorSavingEnableSaveButtonFalseCommand : Command {

    [Inject] private Ref<ILevelEditorSavingSaveButton> levelEditorSavingSaveButtonRef;

    protected override void Execute() {
        levelEditorSavingSaveButtonRef.Get().SetInteractable(false);
    }

}
