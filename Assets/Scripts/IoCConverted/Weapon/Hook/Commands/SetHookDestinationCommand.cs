using IoCPlus;

public class SetHookDestinationCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    [InjectParameter] private FireWeaponParameter fireWeaponParameter;

    protected override void Execute() {
        hookRef.Get().Destination = fireWeaponParameter.destination;
    }
}
