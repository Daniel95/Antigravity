using IoCPlus;
using UnityEngine;

public class SetHookShootDirectionCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    [InjectParameter] private FireWeaponEvent.Parameter FireWeaponEventParameter;

    protected override void Execute() {
        hookRef.Get().ShootDirection = FireWeaponEventParameter.Direction;
    }
}
