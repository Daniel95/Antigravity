using UnityEngine;
using System.Collections;
using IoCPlus;

public class AbortIfChangeSpeedCDIsActive : Command {

    [Inject] private Ref<ICharacterSpeed> characterSpeedRef;

    protected override void Execute() {
        if(characterSpeedRef.Get().ChangeSpeedCDIsActive) {
            Abort();
        }
    }
}
