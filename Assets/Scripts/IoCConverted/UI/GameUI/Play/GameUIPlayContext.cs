using IoCPlus;

public class GameUIPlayContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabInCanvasCommand>("UI/GameUI/PlayUI");

    }

}
