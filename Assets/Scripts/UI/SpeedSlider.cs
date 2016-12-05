using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour {

    [SerializeField]
    private ChangeSpeedMultiplier speedMultiplier;

    private Scrollbar bar;

    // Use this for initialization
    void Start () {
        bar = GetComponent<Scrollbar>();
    }

    public void ValueChange() {
        bar.value = 1;
    }
}
