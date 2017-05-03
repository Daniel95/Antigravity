﻿using IoCPlus;

public class HookProjectileGoToDestinationCommand : Command {

    [Inject] private Ref<IMoveTowards> moveTowardsRef;

    [InjectParameter] private FireWeaponParameter fireWeaponParameter;

    protected override void Execute() {
        moveTowardsRef.Get().StartMoving(fireWeaponParameter.destination);
    }
}
