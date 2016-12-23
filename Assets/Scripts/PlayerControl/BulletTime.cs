using UnityEngine;
using System.Collections;

public class BulletTime : MonoBehaviour {

    [SerializeField]
    private LineRenderer aimRay;

    [SerializeField]
    private float bulletTimeTimeScale = 0.3f;

    [SerializeField]
    private float slowDownTime = 0.1f;

    private Vector2 rayDestination;

    private Coroutine updateLineRendererPositions;

    private Coroutine moveTimeScale;

    private bool bulletTimeActive;

    void Start() {
        aimRay.enabled = false;
    }

    public void StartBulletTime() {
        if (moveTimeScale != null) {
            StopCoroutine(moveTimeScale);
        }

        bulletTimeActive = true;
        aimRay.enabled = true;

        updateLineRendererPositions = StartCoroutine(UpdateLineRendererPositions());
        moveTimeScale = StartCoroutine(MoveTimeScale(bulletTimeTimeScale, slowDownTime));
    }

    IEnumerator UpdateLineRendererPositions() {
        while (true)
        {
            aimRay.SetPosition(0, transform.position);
            aimRay.SetPosition(1, rayDestination);
            yield return null;
        }
    }

    IEnumerator MoveTimeScale(float _target, float _time)
    {
        while (Time.timeScale != _target)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, _target, _time);
            yield return new WaitForFixedUpdate();
        }

    }

    public void StopBulletTime()
    {
        if (bulletTimeActive) {

            StopCoroutine(moveTimeScale);
            StopCoroutine(updateLineRendererPositions);

            bulletTimeActive = false;
            aimRay.enabled = false;

            moveTimeScale = StartCoroutine(MoveTimeScale(1, slowDownTime));
            Time.timeScale = 1;
        }
    }

    public Vector2 SetRayDestination {
        set { rayDestination = value; }
    }

    public bool BulletTimeActive {
        get { return bulletTimeActive; }
    }
}
