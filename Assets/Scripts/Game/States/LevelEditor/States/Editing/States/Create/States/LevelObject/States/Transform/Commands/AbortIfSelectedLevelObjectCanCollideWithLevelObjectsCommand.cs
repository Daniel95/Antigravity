using IoCPlus;

public class AbortIfSelectedLevelObjectCanCollideWithLevelObjectsCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectRef.Get().LevelObjectType;
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
        if (generateableLevelObjectNode.CanCollideWithLevelObjects) {
            Abort();
        }
    }

}
