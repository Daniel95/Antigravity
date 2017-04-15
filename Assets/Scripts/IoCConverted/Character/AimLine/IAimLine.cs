using UnityEngine;
using System.Collections;

public interface IAimLine {

    Vector2 LineDestination { set; }
    bool AimLineActive { get; }

    void StartAimLine(Vector2 destination);
    void StopAimLine();
}
