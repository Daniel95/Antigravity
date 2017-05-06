using IoCPlus;

public class InputContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<InputModel>();

        Bind<Ref<IPCInput>>();
        Bind<Ref<IMobileInput>>();

        Bind<ActivateInputPlatformEvent>();

        On<EnterContextSignal>()
            .Do<ActivateViewOnPlayerCommand<ActivateInputPlatformView>>();

        On<ActivateInputPlatformEvent>()
            .Do<ActivateInputPlatformViewCommand>();

        On<RawJumpInputEvent>()
            .Do<AbortIfActionInputIsNotEnabled>()
            .Dispatch<JumpInputEvent>();

        On<RawCancelDragInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Dispatch<CancelDragInputEvent>();

        On<RawHoldingInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Dispatch<HoldingInputEvent>();

        On<RawCancelDragInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Dispatch<CancelDragInputEvent>();

        On<RawReleaseInDirectionInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Do<DispatchReleaseInDirectionEventCommand>();

        On<RawReleaseInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Dispatch<ReleaseInputEvent>();

        On<RawTappedExpiredInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Dispatch<TappedExpiredInputEvent>();
    }

}