using UnityEngine;

public interface ICamera {

    Vector3 StartPosition { get; }
    Vector2 Position { get; set; }
    CameraBounds CameraBounds { get; }

    void SetCameraBounds(CameraBounds cameraBounds);
}
