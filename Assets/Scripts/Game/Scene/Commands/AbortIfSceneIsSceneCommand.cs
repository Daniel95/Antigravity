using IoCPlus;

public class AbortIfSceneIsSceneCommand : Command<Scenes> {

    [InjectParameter] private Scenes scene;

    protected override void Execute(Scenes scene) {
        if(this.scene == scene) {
            Abort();
        }
    }
}
