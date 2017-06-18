using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfPlayerHittingTriggerTagsDoesNotContainCharacterKillerTagsCommand : Command {

    [Inject(Label.Player)] private Ref<ITriggerHitDetection> playerTriggerHitDetectionRef;
    [Inject] private Ref<ICharacterDie> characterDieRef;

    protected override void Execute() {
        List<string> hittingTriggerTags = playerTriggerHitDetectionRef.Get().HittingTriggerTags;
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
