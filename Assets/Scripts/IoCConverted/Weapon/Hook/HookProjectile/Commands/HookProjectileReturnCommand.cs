﻿using IoCPlus;

public class HookProjectileReturnCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [InjectParameter] private FireWeaponParameter fireWeaponParameter;

    protected override void Execute() {
        hookProjectileRef.Get().GoToDestination(fireWeaponParameter.destination);
    }
}