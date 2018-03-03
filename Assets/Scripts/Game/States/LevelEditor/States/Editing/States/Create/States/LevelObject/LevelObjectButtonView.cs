using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelObjectButtonView : View {

    [Inject] private LevelObjectButtonClickedEvent levelObjectButtonClickedEvent;

    private Button button;
    private LevelObjectType levelObjectType;

    public void SetLevelObject(LevelObjectType levelObjectType) {
        this.levelObjectType = levelObjectType;
        GenerateableLevelObjectNode levelObjectEditorNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);

        SpriteRenderer levelObjectEditorNodeSpriteRenderer = levelObjectEditorNode.Prefab.GetComponentInChildren<SpriteRenderer>();

        GetButton().image.sprite = levelObjectEditorNodeSpriteRenderer.sprite;
        GetButton().image.color = levelObjectEditorNodeSpriteRenderer.color;
    }

    private void OnClicked() {
        levelObjectButtonClickedEvent.Dispatch(levelObjectType);
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
