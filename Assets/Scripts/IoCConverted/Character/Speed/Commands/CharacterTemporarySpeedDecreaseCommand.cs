﻿using IoCPlus;

public class CharacterTemporarySpeedDecreaseCommand : Command {

    [Inject] private Ref<ICharacterSpeed> characterSpeedRef;

    [Inject] private CharacterTemporarySpeedChangeEvent temporarySpeedChangeEvent;

    protected override void Execute() {
        temporarySpeedChangeEvent.Dispatch(new CharacterTemporarySpeedChangeParameter(0.5f - characterSpeedRef.Get().SpeedBoostValue));
    }
}