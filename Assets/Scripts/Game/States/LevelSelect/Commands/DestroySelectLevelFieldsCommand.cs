using IoCPlus;
using UnityEngine;

public class DestroySelectLevelFieldsCommand : Command {

    [Inject] private Ref<ISelectableLevelField> selectableLevelFieldRef;

    protected override void Execute() {
        selectableLevelFieldRef.Get().DestroySelectableLevelFields();
    }
}
