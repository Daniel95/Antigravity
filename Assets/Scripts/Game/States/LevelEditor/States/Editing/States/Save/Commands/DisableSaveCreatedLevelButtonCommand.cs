using IoCPlus;
using UnityEngine;

public class DisableSaveCreatedLevelButtonCommand : Command {

    [Inject] private Ref<ISaveCreatedLevelButton> saveCreatedLevelButtonRef;

    protected override void Execute() {
        saveCreatedLevelButtonRef.Get().SetInteractable(false);
    }

}
