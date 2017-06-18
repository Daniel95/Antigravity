using IoCPlus;
using UnityEngine;

public class AbortIfPlayerNotCollidingAndNotInTriggerWithTagCommand : Command<string> {

    [Inject(Label.Player)] private Ref<ITriggerHitDetection> playerTriggerHitDetectionRef;

    [InjectParameter] private Collision2D collision;

    protected override void Execute(string tag) {
        if (!collision.transform.CompareTag(tag) && !playerTriggerHitDetectionRef.Get().HittingTriggerTags.Contains(tag)) {
            Abort();
        }
    }
}
