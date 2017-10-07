using IoCPlus;

public class LevelEditorInstantiateLevelObjectButtonsCommand : Command<string> {

    [Inject] private Ref<ILevelEditorLevelObjectButtonGridLayoutGroup> levelObjectButtonGridLayoutGroupRef;

    protected override void Execute(string prefabPath) {
        levelObjectButtonGridLayoutGroupRef.Get().InstantiateLevelObjectButtons(prefabPath);
    }

}
