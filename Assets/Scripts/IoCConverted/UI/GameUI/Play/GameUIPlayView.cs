using IoCPlus;

public class GameUIPlayView : View, IGameUIPause {

    [Inject] private Ref<IGameUIPause> gameUIPausedMenu;

    public override void Initialize() {
        gameUIPausedMenu.Set(this);
    }

    public override void Dispose() {
        Destroy(gameObject);
    }

}


