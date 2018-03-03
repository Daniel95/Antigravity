using IoCPlus;

public class DispatchSelectionFieldChangedEventCommand : Command {

    [Inject] private SelectionFieldChangedEvent selectionFieldChangedEvent;

    protected override void Execute() {
        SelectionFieldChangedEvent.Parameter selectionFieldChangedEventParameter = new SelectionFieldChangedEvent.Parameter {
            SelectionFieldStartPosition = SelectionFieldStatusView.SelectionFieldStartGridPosition,
            SelectionFieldEndPosition = SelectionFieldStatusView.SelectionFieldEndGridPosition,
        };
        selectionFieldChangedEvent.Dispatch(selectionFieldChangedEventParameter);
    }

}
