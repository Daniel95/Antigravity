using UnityEngine;

public interface ICamera {

    Vector2 Position { get; set; }
    CameraBounds CameraBounds { get; }

    void SetCameraBounds(CameraBounds cameraBounds);
}
