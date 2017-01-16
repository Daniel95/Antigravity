using UnityEngine;
using System.Collections;

public class TimeManagement : MonoBehaviour {

    [SerializeField]
    private bool changeTimeAtStart = true;

    [SerializeField]
    private int startTimeScale = 0;

    // Use this for initialization
    void Awake() {
        if (changeTimeAtStart)
        {
            Time.timeScale = startTimeScale;
        }
    }

    public void SetTimeScale(int time) {
        Time.timeScale = time;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    public static bool isPauzed()
    {
        return Time.timeScale <= 0;
    }
}
