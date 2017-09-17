using IoCPlus;

public class AbortIfCharacterCountIsLowerThenIntCommand : Command<int> {

    [InjectParameter] private string text;

    protected override void Execute(int characterCount) {
        if(text.Length < characterCount) {
            Abort();
        }
    }

}
