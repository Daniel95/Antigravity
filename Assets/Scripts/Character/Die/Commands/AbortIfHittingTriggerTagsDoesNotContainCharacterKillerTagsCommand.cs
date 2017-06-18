using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfHittingTriggerTagsDoesNotContainCharacterKillerTagsCommand : Command {

    [Inject] private Ref<ITriggerHitDetection> triggerHitDetectionRef;
    [Inject] private Ref<ICharacterDie> characterDieRef;

    protected override void Execute() {
        List<string> hittingTriggerTags = triggerHitDetectionRef.Get().HittingTriggerTags;
        List<string> deadlyTags = characterDieRef.Get().DeadlyTags;

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
