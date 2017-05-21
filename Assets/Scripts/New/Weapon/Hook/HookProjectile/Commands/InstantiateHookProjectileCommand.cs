using UnityEngine;
using IoCPlus;

public class InstantiateHookProjectileCommand : Command {

    [Inject] IContext context;

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowards;

    private const string prefabPath = "Characters/Projectiles/HookProjectile";

    protected override void Execute() {
        View prefab = Resources.Load<View>(prefabPath);
        if (prefab == null) {
            Debug.Log("Can't instantiate view prefab as no prefab is found at given path '" + prefabPath + "'.");
            return;
        }
        View view = context.InstantiateView(prefab);

        moveTowards.Set(view.GetComponent<MoveTowardsView>());
    }

}