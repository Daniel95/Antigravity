﻿using IoCPlus;
using UnityEngine;

public class CharacterStopAimLineCommand : Command {

    [Inject] private Ref<ICharacterAimLine> aimLineRef;

    protected override void Execute() {
        aimLineRef.Get().StopAimLine();
    }
}
