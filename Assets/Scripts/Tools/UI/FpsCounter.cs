using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FpsCounter : MonoBehaviour {
    
    private Text text;

    public float updateInterval = 0.5f;
    private float accumulated = 0;
    private int frames = 0;
    private float timeleft;

    void Awake () {
        text = GetComponent<Text>();
	}

    private void Update() {
        timeleft -= Time.deltaTime;
        accumulated += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0) {
            float fps = accumulated / frames;
            text.text = Mathf.RoundToInt(fps).ToString();

            if (fps < 30) {
                text.color = Color.yellow;
            } else if (fps < 10) {
                text.color = Color.red;
            } else {
                text.color = Color.green;
            }

            timeleft = updateInterval;
            accumulated = 0.0f;
            frames = 0;
        }
    }
    
}
