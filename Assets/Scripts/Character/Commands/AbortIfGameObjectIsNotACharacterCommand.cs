using IoCPlus;
using UnityEngine;

public class AbortIfGameObjectIsNotACharacterCommand : Command {

    [InjectParameter] private GameObject gameObject;

    protected override void Execute() {
        if(!Tags.CharacterTags.Contains(gameObject.tag)) {
            Abort();
        }
    }
}
