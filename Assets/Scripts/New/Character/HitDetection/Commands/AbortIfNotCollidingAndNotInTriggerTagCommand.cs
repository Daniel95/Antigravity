using IoCPlus;
using UnityEngine;

public class AbortIfNotCollidingAndNotInTriggerTagCommand : Command<string> {

    [Inject] private Ref<ICharacterTriggerHitDetection> characterTriggerHitDetectionRef;

    [InjectParameter] private Collision2D collision;

    protected override void Execute(string tag) {
        if (!collision.transform.CompareTag(tag) && !characterTriggerHitDetectionRef.Get().CurrentTriggerTags.Contains(tag)) {
            Abort();
        }
    }
}
