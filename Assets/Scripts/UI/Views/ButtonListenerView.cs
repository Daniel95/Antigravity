using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonListenerView : ListenerView {

    private Button button;

    private void OnEnable() {
        GetButton();
        button.onClick.AddListener(DispatchSignal);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(DispatchSignal);
    }

    private Button GetButton() {
        if(button == null) {
            button = GetComponent<Button>();
        }
        return button;
    }

}
