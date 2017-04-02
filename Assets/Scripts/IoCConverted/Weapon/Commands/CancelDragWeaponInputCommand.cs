using IoCPlus;

public class CancelDragWeaponInputCommand : Command {

    [Inject] private Ref<IWeaponInput> weaponInputRef; 

	protected override void Execute() {
        weaponInputRef.Get().CancelDragging();
    }

}