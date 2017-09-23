using IoCPlus;
using UnityEngine;

public class LevelEditorSetSelectionFieldBoxColorTypeCommand : Command<LevelEditorSelectionFieldBoxColorType> {

    protected override void Execute(LevelEditorSelectionFieldBoxColorType type) {
        Color color = LevelEditorSelectionFieldBoxColors.Instance.GetColorByType(type);
        BoxOverlay.Instance.SetBoxColor(color);
    }

}
