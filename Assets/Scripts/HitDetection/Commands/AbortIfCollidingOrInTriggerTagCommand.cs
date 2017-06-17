using IoCPlus;
using UnityEngine;

public class AbortIfCollidingOrInTriggerTagCommand : Command<string> {

    [Inject] private Ref<ITriggerHitDetection> characterTriggerHitDetectionRef;

    [InjectParameter] private Collision2D collision;

    protected override void Execute(string tag) {
        if (collision.transform.CompareTag(tag) || characterTriggerHitDetectionRef.Get().HittingTriggerTags.Contains(tag)) {
            Abort();
        }
    }
}
