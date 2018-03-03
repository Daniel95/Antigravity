using IoCPlus;
using UnityEngine;

public class TranslateSelectedLevelObjectCommand : Command {

    [Inject] private LevelObjectTranslateStartOffsetStatus levelObjectTranslateStartOffsetStatus;
    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    [InjectParameter] private SwipeMovedOnWorldEvent.Parameter levelEditorSwipeMovedOnWorldEventParameter;

    protected override void Execute() {
        Vector2 worldPosition = levelEditorSwipeMovedOnWorldEventParameter.Position;
        Vector2 startOffset = levelObjectTranslateStartOffsetStatus.StartOffset;

        selectedLevelObjectRef.Get().Translate(worldPosition + startOffset);

    }

}
