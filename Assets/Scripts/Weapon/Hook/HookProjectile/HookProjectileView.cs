using IoCPlus;
using UnityEngine;

public class HookProjectileView : View, IHookProjectile {

    public float DistanceFromOwner { get { return distanceFromOwner; } set { distanceFromOwner = value; } }
    public Transform Transform { get { return transform; } }
    public Transform CollidingTransform { get { return collidingTransform; } set { collidingTransform = value; } }
    public int CollidingTransformLayer {
        get {
            if(collidingTransform == null) { return 0; }
            return collidingTransform.gameObject.layer;
        }
    }

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    private Collider2D myCollider;
    private Transform collidingTransform;
    private float distanceFromOwner;

    public override void Initialize() {
        hookProjectileRef.Set(this);
        gameObject.SetActive(false);
    }

    public void SetParent(Transform parent) {
        transform.SetParent(parent, true);
    }

    public void SetPosition(Vector2 position) {
        transform.position = position;
    }

    public void SetColliderEnabled(bool enabled) {
        myCollider.enabled = enabled;
    }

    public void DestroyProjectile() {
        Destroy();
    }

    public void ActivateHookProjectile(Vector2 startPosition) {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public void DeactivateHookProjectile() {
        gameObject.SetActive(false);
    }

    private void Awake() {
        myCollider = GetComponent<Collider2D>();
    }
}
