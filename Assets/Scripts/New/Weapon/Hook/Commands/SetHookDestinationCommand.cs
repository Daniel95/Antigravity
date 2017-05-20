using IoCPlus;
using UnityEngine;

public class SetHookDestinationCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    [InjectParameter] private FireWeaponEvent.Parameter shootHookEventParameter;

    protected override void Execute() {
        hookRef.Get().Destination = shootHookEventParameter.Destination;
    }
}
