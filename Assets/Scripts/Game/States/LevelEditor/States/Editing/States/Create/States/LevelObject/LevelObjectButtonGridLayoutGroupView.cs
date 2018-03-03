using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LevelObjectButtonGridLayoutGroupView : View, ILevelObjectButtonGridLayoutGroup {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    [Inject] private Ref<ILevelObjectButtonGridLayoutGroup> levelObjectButtonGridLayoutGroup;

    public override void Initialize() {
        base.Initialize();
        levelObjectButtonGridLayoutGroup.Set(this);
    }

    public void InstantiateLevelObjectButtons(string prefabPath) {
        LevelObjectButtonView levelObjectButtonViewPrefab = Resources.Load<LevelObjectButtonView>(prefabPath);

        if(levelObjectButtonViewPrefab == null) {
            Debug.LogWarning("Can't find levelObjectButtonPrefab at path " + prefabPath);
            return;
        }

        foreach (GenerateableLevelObjectNode levelObjectEditorNode in GenerateableLevelObjectLibrary.GenerateableLevelObjectNodes) {
            LevelObjectButtonView levelObjectButtonView = context.InstantiateView(levelObjectButtonViewPrefab);
            levelObjectButtonView.transform.SetParent(transform, false);
            levelObjectButtonView.SetLevelObject(levelObjectEditorNode.LevelObjectType);
        }
    }

}