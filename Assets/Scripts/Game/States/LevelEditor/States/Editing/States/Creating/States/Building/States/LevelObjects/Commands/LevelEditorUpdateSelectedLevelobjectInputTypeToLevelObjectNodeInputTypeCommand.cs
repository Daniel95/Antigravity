using IoCPlus;

public class LevelEditorUpdateSelectedLevelObjectInputTypeToLevelObjectNodeInputTypeCommand : Command {

    protected override void Execute() {
        LevelObjectInputType levelObjectInputType = LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode.GetDefaultLevelObjectInputType();
        LevelEditorSelectedLevelObjectInputTypeStatus.LevelObjectInputType = levelObjectInputType;
    }

}
