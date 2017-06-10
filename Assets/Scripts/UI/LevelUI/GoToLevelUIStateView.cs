using IoCPlus;
using UnityEngine;

public class GoToLevelUIStateView : View {

    [SerializeField] private LevelUIState levelUIState;

    [Inject] private GoToLevelUIStateEvent goToLevelUIStateEvent;

    public void GoToLevelUIState() {
        goToLevelUIStateEvent.Dispatch(levelUIState);
    }
}
