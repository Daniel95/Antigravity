using IoCPlus;

public class InstantiateLevelObjectButtonsCommand : Command<string> {

    [Inject] private Ref<ILevelObjectButtonGridLayoutGroup> levelObjectButtonGridLayoutGroupRef;

    protected override void Execute(string prefabPath) {
        levelObjectButtonGridLayoutGroupRef.Get().InstantiateLevelObjectButtons(prefabPath);
    }

}
