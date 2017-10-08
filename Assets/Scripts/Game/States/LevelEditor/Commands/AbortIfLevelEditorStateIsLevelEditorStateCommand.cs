using IoCPlus;

public class AbortIfLevelEditorStateIsLevelEditorStateCommand : Command<LevelEditorState> {

    [InjectParameter] private LevelEditorState levelEditorState;

    protected override void Execute(LevelEditorState levelEditorState) {
        if (this.levelEditorState == levelEditorState) {
            Abort();
        }
    }

}
