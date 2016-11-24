using UnityEngine;
using System.Collections;

public class TimeManagement : MonoBehaviour {

    [SerializeField]
    private int startTimeScale = 0;

    private int lastTimeScale = 1;

	// Use this for initialization
	void Start () {
        Time.timeScale = startTimeScale;
	}

    public void SwitchTimeScale() {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = lastTimeScale;
        }
    }

    public void SetTimeScale(int _time) {
        Time.timeScale = lastTimeScale = _time;
    }
}
