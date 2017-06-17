using IoCPlus;
using UnityEngine;

public class AbortIfCollidingTagIsCharacterKillerTagCommand : Command {

    [Inject] private Ref<ICharacterDie> characterDieRef;

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        if(!characterDieRef.Get().DeadlyTags.Contains(collision.transform.tag)) {
            Abort();
        }
    }
}
