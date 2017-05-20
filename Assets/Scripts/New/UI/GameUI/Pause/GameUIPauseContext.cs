using IoCPlus;

public class GameUIPauseContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabInCanvasCommand>("UI/GameUI/PauseUI")
            .Do<PauseTimeCommand>(true);

    }

}