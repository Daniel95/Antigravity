using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectCannotCollideWithTilesCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(selectedLevelObjectRef.Get().GameObject.name);
        if (!generateableLevelObjectNode.CanCollideWithTiles) {
            Abort();
        }
    }

}
