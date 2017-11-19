using UnityEngine;

public interface ILevelObject {

    void Select();
    void Unselect();
    void Translate(Vector2 destination); 
    void Scale(Vector2 change);
    void Rotate(Vector2 change);
    void DestroyLevelObject();

    GameObject GameObject { get; }
    Rigidbody2D Rigidbody { get; }
    Collider2D Collider { get; }
    LevelObjectType LevelObjectType { get; }

}
