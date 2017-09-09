using IoCPlus;

public class LevelEditorContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToLevelEditorStateEvent>();

        On<EnterContextSignal>()
            .GotoState<LevelEditorEditContext>();

    }

}