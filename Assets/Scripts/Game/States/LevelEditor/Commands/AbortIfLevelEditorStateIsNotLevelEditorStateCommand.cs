using IoCPlus;

public class AbortIfLevelEditorStateIsNotLevelEditorStateCommand : Command<LevelEditorState> {

    [InjectParameter] private LevelEditorState levelEditorState;

    protected override void Execute(LevelEditorState levelEditorState) {
        if (this.levelEditorState != levelEditorState) {
            Abort();
        }
    }

}
