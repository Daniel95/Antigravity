using IoCPlus;
using UnityEngine;

public class UpdateTranslateStartOffsetPositionCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;
    [Inject] private LevelObjectTranslateStartOffsetStatus levelobjectTranslateStartOffsetStatus;

    protected override void Execute() {
        Vector2 selectedLevelObjectPosition = selectedLevelObjectRef.Get().GameObject.transform.position;
        Vector2 offset = selectedLevelObjectPosition - TranslateStartPositionStatus.StartWorldPosition;
        levelobjectTranslateStartOffsetStatus.StartOffset = offset;
    }

}
