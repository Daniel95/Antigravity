using IoCPlus;
using UnityEngine;

public class AbortIfGameObjectIsNotHookProjectileCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [InjectParameter] private GameObject gameObject;

    protected override void Execute() {
        if(gameObject != hookProjectileRef.Get().Transform.gameObject) {
            Abort();
        }
    }

}
