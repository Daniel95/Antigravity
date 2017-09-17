using IoCPlus;
using UnityEngine;

public class LevelEditorSavingEnableSaveButtonCommand : Command<bool> {

    [Inject] private Ref<ILevelEditorSavingSaveButton> levelEditorSavingSaveButtonRef;

    protected override void Execute(bool enable) {
        levelEditorSavingSaveButtonRef.Get().SetInteractable(enable);
    }

}
