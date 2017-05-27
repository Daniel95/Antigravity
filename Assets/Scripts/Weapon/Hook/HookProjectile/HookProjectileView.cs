using IoCPlus;
using UnityEngine;

public class HookProjectileView : View, IHookProjectile {

    public Transform Transform { get { return transform; } }
    public Transform CollidingTransform { get { return collidingTransform; } set { collidingTransform = value; } }
    public int CollidingTransformLayer {
        get {
            if(collidingTransform == null) { return 0; }
            return collidingTransform.gameObject.layer;
        }
    }

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    private Transform collidingTransform;
    private int hookedLayer;
    private int anchorIndex;
    private bool isMovingTowardsOwner;

    public override void Initialize() {
        hookProjectileRef.Set(this);
        gameObject.SetActive(false);
    }

    public void SetParent(Transform parent) {
        transform.SetParent(parent, true);
    }

    public void DestroyProjectile() {
        Destroy();
    }

    public void ActivateHookProjectile(Vector2 spawnPosition) {
        gameObject.SetActive(true);
        transform.position = spawnPosition;
    }

    public void DeactivateHookProjectile() {
        gameObject.SetActive(false);
    }
}
