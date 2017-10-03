using IoCPlus;
using UnityEngine;

public class PlayerStopAimLineCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterAimLine> playerAimLineRef;

    protected override void Execute() {
        playerAimLineRef.Get().StopAimLine();
    }
}
