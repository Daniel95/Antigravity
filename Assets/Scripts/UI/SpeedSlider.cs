using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpeedSlider : MonoBehaviour, IDragHandler {

    [SerializeField]
    private SpeedMultiplier speedMultiplier;

    private Scrollbar bar;
    
    // Use this for initialization
    void Start () {
        bar = GetComponent<Scrollbar>();
    }

    public void OnDrag(PointerEventData data)
    {
        speedMultiplier.SetSpeedMultiplierPercentage(bar.value);
    }
}
