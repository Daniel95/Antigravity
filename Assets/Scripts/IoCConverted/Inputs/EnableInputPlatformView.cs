using IoCPlus;
using UnityEngine;

public class EnableInputPlatformView : View {

    [Inject] private EnableInputPlatformEvent enableInputEvent;
    [Inject] private InputModel inputModel;

    [SerializeField] private MobileInputView mobileInputView;
    [SerializeField] private PCInputView pcInputView;

    public override void Initialize() {
        enableInputEvent.Dispatch(false);

        if (Platform.PlatformIsMobile()) {
            inputModel.activeInputPlatform = mobileInputView;
        } else {
            inputModel.activeInputPlatform = pcInputView;
        }
        enableInputEvent.Dispatch(true);

    }
}