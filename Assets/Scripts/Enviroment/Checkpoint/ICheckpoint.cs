using UnityEngine;

public interface ICheckpoint {

    bool Unlocked { get; set; }
    GameObject CheckpointGameObject { get; }
    GameObject CheckpointBoundaryGameObject { get; }

    void CheckpointUnlockedEffect();

}
