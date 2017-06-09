using IoCPlus;

public class AbortIfSceneIsNotSceneCommand : Command<Scenes> {

    [InjectParameter] private Scenes scene;

    protected override void Execute(Scenes scene) {
        if(this.scene != scene) {
            Abort();
        }
    }
}
