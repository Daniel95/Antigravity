using IoCPlus;
using UnityEngine;

public class SetSelectionFieldBoxColorTypeCommand : Command<LevelEditorSelectionFieldBoxColorType> {

    protected override void Execute(LevelEditorSelectionFieldBoxColorType type) {
        Color color = SelectionFieldColors.Instance.GetColorByType(type);
        BoxOverlay.Instance.SetBoxColor(color);
    }

}
