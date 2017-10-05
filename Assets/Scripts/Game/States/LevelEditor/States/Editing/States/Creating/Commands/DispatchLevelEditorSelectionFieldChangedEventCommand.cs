using IoCPlus;

public class DispatchLevelEditorSelectionFieldChangedEventCommand : Command {

    [Inject] private LevelEditorSelectionFieldChangedEvent selectionFieldChangedEvent;

    protected override void Execute() {
        LevelEditorSelectionFieldChangedEvent.Parameter selectionFieldChangedEventParameter = new LevelEditorSelectionFieldChangedEvent.Parameter {
            SelectionFieldStartPosition = LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition,
            SelectionFieldEndPosition = LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition,
        };
        selectionFieldChangedEvent.Dispatch(selectionFieldChangedEventParameter);
    }

}
