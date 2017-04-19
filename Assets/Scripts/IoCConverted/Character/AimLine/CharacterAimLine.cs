﻿using IoCPlus;
using System.Collections;
using UnityEngine;

public class CharacterAimLineView : View, ICharacterAimLine {

    public bool AimLineActive { get { return aimLineActive; } }

    [SerializeField] private LineRenderer line;

    private Vector2 lineDestination;
    private Coroutine updateLineRendererPositions;
    private bool aimLineActive;

    public void UpdateAimLineDestination(Vector2 destination) {
        if (!aimLineActive) {
            StartAimLine(destination);
        }

        lineDestination = destination;
    }

    public void StopAimLine() {
        if (aimLineActive) {
            StopCoroutine(updateLineRendererPositions);

            line.enabled = aimLineActive = false;
        }
    }

    void Start() {
        line.enabled = false;
    }

    private void StartAimLine(Vector2 destination) {
        line.enabled = aimLineActive = true;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, destination);

        updateLineRendererPositions = StartCoroutine(UpdateLineRendererPositions());
    }

    IEnumerator UpdateLineRendererPositions() {
        while (true)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, lineDestination);
            yield return null;
        }
    }
}
