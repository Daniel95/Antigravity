using UnityEngine;

public interface ICamera {

    Vector2 WorldPosition { get; set; }
    CameraBounds CameraBounds { get; }

    void SetCameraBounds(CameraBounds cameraBounds);
}
