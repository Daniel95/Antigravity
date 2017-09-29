using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelEditorLevelObjectButton : View {

    [Inject] private LevelEditorSelectedLevelObjectNodeTypeStatus levelEditorSelectedLevelObjectStatus;

    private Button button;
    private LevelObjectType levelObjectType;

    public void SetLevelObject(LevelObjectType levelObjectType) {
        this.levelObjectType = levelObjectType;
        LevelEditorLevelObjectEditorNode levelObjectEditorNode = LevelEditorLevelObjectEditorNodesContainer.Instance.GetNode(levelObjectType);
        GetButton().image.sprite = levelObjectEditorNode.Prefab.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnClicked() {
        levelEditorSelectedLevelObjectStatus.LevelObjectType = levelObjectType;
    }

    private void OnEnable() {
        GetButton().onClick.AddListener(OnClicked);
    }

    private void OnDisable() {
        GetButton().onClick.RemoveListener(OnClicked);
    }

    private Button GetButton() {
        if(button == null) {
            button = GetComponent<Button>();
        }
        return button;
    }

}
