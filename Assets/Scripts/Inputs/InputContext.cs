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
            .Do<AbortIfActionInputIsNotEnabled>()
            .Dispatch<JumpInputEvent>();

        On<RawCancelDragInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Dispatch<CancelDragInputEvent>();

        On<RawHoldingInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Dispatch<HoldingInputEvent>();

        On<RawDraggingInputEvent>()
            .Do<AbortIfShootingInputIsNotEnabled>()
            .Do<DispatchDraggingInputEventCommand>();

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