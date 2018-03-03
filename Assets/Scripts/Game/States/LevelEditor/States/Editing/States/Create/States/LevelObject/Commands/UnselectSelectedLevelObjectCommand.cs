using IoCPlus;

public class UnselectSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        selectedLevelObjectRef.Get().Unselect();
    }

}
