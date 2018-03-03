using IoCPlus;
using UnityEngine;

public class EnableSaveCreatedLevelButtonCommand : Command<bool> {

    [Inject] private Ref<ISaveCreatedLevelButton> levelEditorSavingSaveButtonRef;

    protected override void Execute(bool enable) {
        levelEditorSavingSaveButtonRef.Get().SetInteractable(enable);
    }

}
