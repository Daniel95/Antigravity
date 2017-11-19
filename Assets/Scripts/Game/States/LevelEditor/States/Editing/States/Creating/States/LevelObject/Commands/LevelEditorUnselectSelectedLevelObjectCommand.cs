using IoCPlus;

public class LevelEditorUnselectSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        selectedLevelObjectRef.Get().Unselect();
    }

}
