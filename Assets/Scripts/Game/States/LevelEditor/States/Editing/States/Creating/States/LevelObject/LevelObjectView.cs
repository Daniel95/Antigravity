using IoCPlus;
using UnityEngine;

public class LevelObjectView : View, ILevelObject {

    public Rigidbody2D Rigidbody { get { return rigidbody; } }
    public GameObject GameObject { get { return gameObject; } }
    public Collider2D Collider { get { return collider; } }

    [Inject] IContext context;
    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;
    [Inject(Label.PreviousSelectedLevelObject)] private Ref<ILevelObject> previousSelectedLevelObjectRef;

    [Inject] private LevelEditorNewLevelObjectSelectedEvent selectedLevelObjectStatusUpdatedEvent;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    public void Select() {
        if(this == (Object)selectedLevelObjectRef.Get()) { return; }
        if(selectedLevelObjectRef.Get() != null) {
            selectedLevelObjectRef.Get().Unselect();
        }

        AddRigidBody();
        AddCollisionHitDetectionView();

        selectedLevelObjectRef.Set(this);
        selectedLevelObjectStatusUpdatedEvent.Dispatch(this);
    }

    public void Unselect() {
        previousSelectedLevelObjectRef.Set(this);
        selectedLevelObjectRef.Set(null);
        RemoveRigidBody();
        RemoveCollisionHitDetectionView();
    }

    public virtual void Translate(Vector2 destination) {
        rigidbody.MovePosition(destination);
        rigidbody.velocity = Vector2.zero;
    }

    public virtual void Scale(Vector2 change) {
        transform.localScale += (Vector3)change;
    }

    public virtual void Rotate(Vector2 change) {
        throw new System.NotImplementedException();
    }
    
    private void AddRigidBody() {
        rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void RemoveRigidBody() {
        Object.Destroy(rigidbody);
    }

    private void AddCollisionHitDetectionView() {
        CollisionHitDetectionView collisionHitDetectionView = gameObject.AddComponent<CollisionHitDetectionView>();
        context.AddView(collisionHitDetectionView, false);
    }

    private void RemoveCollisionHitDetectionView() {
        CollisionHitDetectionView collisionHitDetectionView = gameObject.GetComponent<CollisionHitDetectionView>();
        collisionHitDetectionView.Destroy(true);
    }

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

}
