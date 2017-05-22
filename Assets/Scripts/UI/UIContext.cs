using IoCPlus;

public class UIContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<CanvasModel>();

        On<EnterContextSignal>()
            .Do<InstantiateCanvasCommand>()
            .AddContext<GameUIContext>();

    }

}