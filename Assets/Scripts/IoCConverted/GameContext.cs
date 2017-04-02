using IoCPlus;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewPrefabCommand>("Resources/Characters/Player")
            .AddContext<PlayerStateContext>();
    }

}