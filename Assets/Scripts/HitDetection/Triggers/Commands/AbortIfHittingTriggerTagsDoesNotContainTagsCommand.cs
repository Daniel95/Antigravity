using IoCPlus;
using System.Collections.Generic;

public class AbortIfHittingTriggerTagsDoesNotContainTagsCommand : Command<List<string>> {

    [Inject] private Ref<ITriggerHitDetection> triggerHitDetectionRef;

    protected override void Execute(List<string> tags) {
        List<string> hittingTriggerTags = triggerHitDetectionRef.Get().HittingTriggerTags;

        foreach (string hittingTag in hittingTriggerTags) {
            if(tags.Contains(hittingTag)) {
                Abort();
                return;
            }
        }
    }

}
