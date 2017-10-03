using IoCPlus;
using System.Collections.Generic;

public class AbortIfPlayerHittingTriggerTagsDoesNotContainTagsCommand : Command<List<string>> {

    [Inject(Label.Player)] private Ref<ITriggerHitDetection> playerTriggerHitDetectionRef;

    protected override void Execute(List<string> tags) {
        List<string> hittingTriggerTags = playerTriggerHitDetectionRef.Get().HittingTriggerTags;

        foreach (string hittingTag in hittingTriggerTags) {
            if(tags.Contains(hittingTag)) {
                Abort();
                return;
            }
        }
    }

}
