using UnityEngine;

public interface ICharacterAimLine {

    bool AimLineActive { get; }

    void UpdateAimLineDestination(Vector2 destination);
    void StopAimLine();
}
