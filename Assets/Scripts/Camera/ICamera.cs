using UnityEngine;

public interface ICamera {

    Vector2 WorldPosition { get; set; }
    CameraBounds CameraBounds { get; }
    float OrthographicSize { get; set; }
    float OrthographicSizeRatio { get; }
    float MaxOrthographicSize { get; }
    float MinOrthographicSize { get; }

    void SetCameraBounds(CameraBounds cameraBounds);
}
