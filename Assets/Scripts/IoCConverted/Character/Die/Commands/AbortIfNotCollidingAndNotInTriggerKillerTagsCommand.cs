﻿using IoCPlus;
using UnityEngine;

public class AbortIfNotCollidingAndNotInTriggerKillerTagsCommand : Command {

    [Inject] private Ref<ICharacterDie> characterDieRef;

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        if(!characterDieRef.Get().KillerTag.Contains(collision.transform.tag)) {
            Abort();
        }
    }
}
