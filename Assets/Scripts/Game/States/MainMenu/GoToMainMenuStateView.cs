using IoCPlus;
using UnityEngine;

public class GoToMainMenuStateView : View {

    [SerializeField] private MainMenuState mainMenuState;

    [Inject] private GoToMainMenuStateEvent goToMainMenuStateEvent;

    public void GoToMainMenuState() {
        goToMainMenuStateEvent.Dispatch(mainMenuState);
    }
}
