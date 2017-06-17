using IoCPlus;
using UnityEngine;

public class AbortIfNotCollidingAndNotInTriggerTagCommand : Command<string> {

    [Inject] private Ref<ITriggerHitDetection> triggerHitDetectionRef;

    [InjectParameter] private Collision2D collision;

    protected override void Execute(string tag) {
        if (!collision.transform.CompareTag(tag) && !triggerHitDetectionRef.Get().HittingTriggerTags.Contains(tag)) {
            Abort();
        }
    }
}
