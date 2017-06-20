using IoCPlus;
using UnityEngine;

public class AbortIfPlayerCollidingTagIsPlayerKillerTagCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterDie> playerDieRef;

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        if(!playerDieRef.Get().DeadlyTags.Contains(collision.transform.tag)) {
            Abort();
        }
    }
}
