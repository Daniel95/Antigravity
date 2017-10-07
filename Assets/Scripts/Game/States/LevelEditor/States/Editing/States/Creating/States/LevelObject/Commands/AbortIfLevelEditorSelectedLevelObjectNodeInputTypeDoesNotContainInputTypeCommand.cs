using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeInputTypeDoesNotContainInputTypeCommand : Command<LevelObjectInputType> {

    protected override void Execute(LevelObjectInputType levelEditorLevelObjectInputType) {
        if(!LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode.AvailableInputTypes.Contains(levelEditorLevelObjectInputType)) {
            Abort();
        }
    }

}
