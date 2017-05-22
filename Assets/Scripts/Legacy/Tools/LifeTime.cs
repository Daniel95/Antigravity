using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour {

    private int _timeToLive = 500;
	
	// Update is called once per frame
	void FixedUpdate () {
        _timeToLive--;
        if (_timeToLive < 0)
            Destroy(gameObject);
    }

    public int TimeToLive {
        set { _timeToLive = value; }
    }
}
