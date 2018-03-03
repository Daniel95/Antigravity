using IoCPlus;
using System.IO;
using UnityEngine;

public class InstantiateLevelSelectButtonsCommand : Command {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;
    [Inject] private Ref<ILevelSelectGridLayoutGroup> levelEditorLevelSelectGridLayoutGroupRef;

    private const string LEVEL_SELECT_BUTTON_PREFAB_PATH = "UI/LevelEditor/LevelSelect/LevelEditorLevelSelectButtonUI";

    protected override void Execute() {
        LevelSelectButtonView prefab = Resources.Load<LevelSelectButtonView>(LEVEL_SELECT_BUTTON_PREFAB_PATH);
        if (prefab == null) {
            Debug.LogWarning("Can't instantiate LevelEditorLevelSelectButtonView prefab as no prefab is found at given path '" + LEVEL_SELECT_BUTTON_PREFAB_PATH + "'.");
            return;
        }

        if (!Directory.Exists(LevelDataPath.Path)) { return; }

        DirectoryInfo info = new DirectoryInfo(LevelDataPath.Path);
        FileInfo[] filesInfo = info.GetFiles();
        foreach (FileInfo fileInfo in filesInfo) {
            LevelSelectButtonView levelEditorLevelSelectButtonView = context.InstantiateView(prefab);
            levelEditorLevelSelectButtonView.transform.SetParent(levelEditorLevelSelectGridLayoutGroupRef.Get().Transform);

            string levelName = StringHelper.RevertXMLCompatible(fileInfo.Name); 
            levelEditorLevelSelectButtonView.SetButtonName(levelName);
        }
    }

}