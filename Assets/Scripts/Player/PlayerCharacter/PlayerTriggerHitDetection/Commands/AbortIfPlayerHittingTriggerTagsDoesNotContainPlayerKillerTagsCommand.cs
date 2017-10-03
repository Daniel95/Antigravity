using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfPlayerHittingTriggerTagsDoesNotContainPlayerKillerTagsCommand : Command {

    [Inject(Label.Player)] private Ref<ITriggerHitDetection> playerTriggerHitDetectionRef;
    [Inject(Label.Player)] private Ref<ICharacterDie> playerDieRef;

    protected override void Execute() {
        List<string> hittingTriggerTags = playerTriggerHitDetectionRef.Get().HittingTriggerTags;
        List<string> deadlyTags = playerDieRef.Get().DeadlyTags;

        bool hitDeadlyTag = hittingTriggerTags.Find(x => deadlyTags.Contains(x)) != null;

        if(!hitDeadlyTag) {
            Abort();
        }
    }

}
