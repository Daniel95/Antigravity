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

        Debug.Log("get triggerHitDetectionRef from " + ((View)triggerHitDetectionRef.Get()).gameObject);
        Debug.Log(hittingTriggerTags.Count);

        foreach (string hittingTag in hittingTriggerTags) {
            Debug.Log(hittingTag);
            if(deadlyTags.Contains(hittingTag)) {
                hittingTagsContainsDeadlyTags = true;
            }
        }

        if(!hittingTagsContainsDeadlyTags) {
            Abort();
        }
    }

}
