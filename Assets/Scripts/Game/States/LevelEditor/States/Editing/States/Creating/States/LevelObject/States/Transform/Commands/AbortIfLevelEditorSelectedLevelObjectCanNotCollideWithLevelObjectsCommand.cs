using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectCannotCollideWithLevelObjectsCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(selectedLevelObjectRef.Get().GameObject.name);
        if (!generateableLevelObjectNode.CanCollideWithLevelObjects) {
            Abort();
        }
    }

}
