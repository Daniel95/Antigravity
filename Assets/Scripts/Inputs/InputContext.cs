using IoCPlus;

public class InputContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<ActivateInputPlatformEvent>();

        On<EnterContextSignal>()
            .Dispatch<ActivateInputPlatformEvent>();

        On<ActivateInputPlatformEvent>()
            .Do<AbortIfPlatformIsNotMobileCommand>()
            .Do<EnablePCInputCommand>(false)
            .Do<EnableMobileInputCommand>(true);

        On<ActivateInputPlatformEvent>()
            .Do<AbortIfPlatformIsMobileCommand>()
            .Do<EnableMobileInputCommand>(false)
            .Do<EnablePCInputCommand>(true);

        On<RawJumpInputEvent>()
            .Do<AbortIfInputIsNotEnabledCommand>()
            .Dispatch<JumpInputEvent>();

        On<RawCancelDragInputEvent>()
            .Do<AbortIfInputIsNotEnabledCommand>()
            .Dispatch<CancelDragInputEvent>();

        On<RawHoldingInputEvent>()
            .Do<AbortIfInputIsNotEnabledCommand>()
            .Dispatch<HoldingInputEvent>();

        On<RawDraggingInputEvent>()
            .Do<AbortIfInputIsNotEnabledCommand>()
            .Do<DispatchDraggingInputEventCommand>();

        On<RawReleaseInDirectionInputEvent>()
            .Do<AbortIfInputIsNotEnabledCommand>()
            .Do<DispatchReleaseInDirectionEventCommand>();

        On<RawReleaseInputEvent>()
            .Do<AbortIfInputIsNotEnabledCommand>()
            .Dispatch<ReleaseInputEvent>();

        On<RawTappedExpiredInputEvent>()
            .Do<AbortIfInputIsNotEnabledCommand>()
            .Dispatch<TappedExpiredInputEvent>();
    }

}