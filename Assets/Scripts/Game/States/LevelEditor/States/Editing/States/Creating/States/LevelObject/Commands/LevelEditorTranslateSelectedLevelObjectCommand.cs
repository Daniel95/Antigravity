using IoCPlus;
using UnityEngine;

public class LevelEditorTranslateSelectedLevelObjectCommand : Command {

    [Inject] private LevelEditorLevelObjectTranslateStartOffsetStatus levelObjectTranslateStartOffsetStatus;
    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    [InjectParameter] private LevelEditorSwipeMovedOnWorldEvent.Parameter levelEditorSwipeMovedOnWorldEventParameter;

    protected override void Execute() {
        Vector2 worldPosition = levelEditorSwipeMovedOnWorldEventParameter.Position;
        Vector2 startOffset = levelObjectTranslateStartOffsetStatus.StartOffset;

        selectedLevelObjectRef.Get().Translate(worldPosition + startOffset);

    }

}
