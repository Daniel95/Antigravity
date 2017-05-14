﻿using IoCPlus;

public class ActivateHookProjectileCommand : Command { 

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [InjectParameter] private ShootHookEvent.ShootHookEventParameter shootHookEventParameter;

    protected override void Execute() {
        hookProjectileRef.Get().ActivateHookProjectile(shootHookEventParameter.StartPosition);
    }
}
