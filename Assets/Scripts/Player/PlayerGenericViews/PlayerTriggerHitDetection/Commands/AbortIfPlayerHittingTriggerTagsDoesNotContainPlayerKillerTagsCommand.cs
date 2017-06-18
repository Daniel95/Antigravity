using IoCPlus;
using System.Collections.Generic;

public class AbortIfPlayerHittingTriggerTagsDoesNotContainPlayerKillerTagsCommand : Command {

    [Inject(Label.Player)] private Ref<ITriggerHitDetection> playerTriggerHitDetectionRef;
    [Inject(Label.Player)] private Ref<ICharacterDie> playerDieRef;

    protected override void Execute() {
        List<string> hittingTriggerTags = playerTriggerHitDetectionRef.Get().HittingTriggerTags;
        List<string> deadlyTags = playerDieRef.Get().DeadlyTags;

        bool hittingTagsContainsDeadlyTags = false;

        foreach (string hittingTag in hittingTriggerTags) {
            if(deadlyTags.Contains(hittingTag)) {
                hittingTagsContainsDeadlyTags = true;
            }
        }

        if(!hittingTagsContainsDeadlyTags) {
            Abort();
        }
    }

}
