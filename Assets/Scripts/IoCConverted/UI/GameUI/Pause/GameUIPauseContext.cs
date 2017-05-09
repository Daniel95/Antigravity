using IoCPlus;

public class GameUIPauseContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<Ref<IGameUIPause>>();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabInCanvasCommand>("UI/GameUI/PauseMenu");
    }

}