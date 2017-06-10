using IoCPlus;
using UnityEngine;

public class GoToMainMenuUIStateView : View {

    [SerializeField] private MainMenuUIState mainMenuState;

    [Inject] private GoToMainMenuUIStateEvent goToMainMenuStateEvent;

    public void GoToMainMenuState() {
        goToMainMenuStateEvent.Dispatch(mainMenuState);
    }
}
