using IoCPlus;

public class LevelEditorContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToLevelEditorStateEvent>();

        Bind<LevelNameStatus>();

        On<EnterContextSignal>()
            .GotoState<LevelEditorEditingContext>();

    }

}