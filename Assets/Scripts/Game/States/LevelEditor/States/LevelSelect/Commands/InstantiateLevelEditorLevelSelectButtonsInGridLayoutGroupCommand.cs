using IoCPlus;
using System.IO;
using UnityEngine;

public class InstantiateLevelEditorLevelSelectButtonsInGridLayoutGroupCommand : Command {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;
    [Inject] private Ref<ILevelEditorLevelSelectGridLayoutGroup> levelEditorLevelSelectGridLayoutGroupRef;

    private const string LEVEL_SELECT_BUTTON_PREFAB_PATH = "UI/LevelEditor/LevelSelect/LevelEditorLevelSelectButtonUI";

    protected override void Execute() {
        ILevelEditorLevelSelectGridLayoutGroup levelEditorLevelSelectGridLayoutGroup = levelEditorLevelSelectGridLayoutGroupRef.Get();

        LevelEditorLevelSelectButtonView prefab = Resources.Load<LevelEditorLevelSelectButtonView>(LEVEL_SELECT_BUTTON_PREFAB_PATH);
        if (prefab == null) {
            Debug.LogWarning("Can't instantiate LevelEditorLevelSelectButtonView prefab as no prefab is found at given path '" + LEVEL_SELECT_BUTTON_PREFAB_PATH + "'.");
            return;
        }

        DirectoryInfo info = new DirectoryInfo(LevelEditorLevelDataPath.Path);
        FileInfo[] filesInfo = info.GetFiles();
        foreach (FileInfo fileInfo in filesInfo) {
            LevelEditorLevelSelectButtonView levelEditorLevelSelectButtonView = context.InstantiateView(prefab);
            levelEditorLevelSelectButtonView.transform.SetParent(levelEditorLevelSelectGridLayoutGroup.Transform);

            string levelName = StringHelper.RevertXMLCompatible(fileInfo.Name); 
            levelEditorLevelSelectButtonView.SetButtonName(levelName);
        }
    }

}