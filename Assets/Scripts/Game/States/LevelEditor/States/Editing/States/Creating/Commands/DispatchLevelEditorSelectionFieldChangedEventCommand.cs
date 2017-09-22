using IoCPlus;

public class DispatchLevelEditorSelectionFieldChangedEventCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    [Inject] private LevelEditorSelectionFieldChangedEvent selectionFieldChangedEvent;

    protected override void Execute() {
        LevelEditorSelectionFieldChangedEvent.Parameter selectionFieldChangedEventParameter = new LevelEditorSelectionFieldChangedEvent.Parameter {
            SelectionFieldStartPosition = selectionFieldStatus.SelectionFieldStartGridPosition,
            SelectionFieldEndPosition = selectionFieldStatus.SelectionFieldEndGridPosition,
        };
        selectionFieldChangedEvent.Dispatch(selectionFieldChangedEventParameter);
    }

}
