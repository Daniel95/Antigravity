using UnityEngine;

public interface ICheckpoint {

    GameObject CheckpointGameObject { get; }
    GameObject CheckpointBoundaryGameObject { get; }
}
