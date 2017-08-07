using IoCPlus;
using UnityEngine;

public class GoToLevelEditorStateView : View {

    [SerializeField] private LevelEditorState levelEditorState;

    [Inject] private GoToLevelEditorStateEvent goToLevelEditorStateEvent;

    public void GoToLevelEditorState() {
        goToLevelEditorStateEvent.Dispatch(levelEditorState);
    }

}
