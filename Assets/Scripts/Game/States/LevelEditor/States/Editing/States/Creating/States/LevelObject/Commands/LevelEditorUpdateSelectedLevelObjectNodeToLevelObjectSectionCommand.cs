using IoCPlus;
using UnityEngine;

public class LevelEditorUpdateSelectedLevelObjectNodeToLevelObjectSectionCommand : Command {

    protected override void Execute() {
        LevelObjectSection levelObjectSection = LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection;
        LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectSection.LevelObject.LevelObjectType);
    }

}
