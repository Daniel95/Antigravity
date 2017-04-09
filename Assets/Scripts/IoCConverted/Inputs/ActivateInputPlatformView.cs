using IoCPlus;
using UnityEngine;

public class ActivateInputPlatformView : View {

    [Inject] private InputModel inputModel;

    [Inject] private ActivateInputPlatformEvent activateInputEvent;

    private MobileInputView mobileInputView;
    private PCInputView pcInputView;

    private void Awake() {
        mobileInputView = GetComponent<MobileInputView>();
        pcInputView = GetComponent<PCInputView>();
    }

    public override void Initialize() {
        if (Platform.PlatformIsMobile() && mobileInputView != inputModel.inputPlatform as MobileInputView) {
            activateInputEvent.Dispatch(false);
            inputModel.inputPlatform = mobileInputView;
            activateInputEvent.Dispatch(true);
        } else if(pcInputView != inputModel.inputPlatform as PCInputView){
            activateInputEvent.Dispatch(false);
            inputModel.inputPlatform = pcInputView;
            activateInputEvent.Dispatch(true);
        }
    }
}