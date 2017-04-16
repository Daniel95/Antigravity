﻿using IoCPlus;

public class CharacterEnterKillingTriggerCommand : Command {

    [Inject] private Ref<ICharacterDie> characterJumpRef;

    [InjectParameter] private string killerTag;

    protected override void Execute() {
        characterJumpRef.Get().EnteringKillingTrigger(killerTag);
    }
}
