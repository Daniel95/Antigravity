using IoCPlus;
using System.Collections.Generic;

public class AbortIfSceneIsScenesCommand : Command<List<Scenes>> {

    [InjectParameter] private Scenes scene;

    protected override void Execute(List<Scenes> scenes) {
        foreach (Scenes scene in scenes) {
            if (this.scene == scene) {
                Abort();
            }
        }
    }
}
