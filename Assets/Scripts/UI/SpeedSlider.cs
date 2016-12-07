using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpeedSlider : MonoBehaviour, IDragHandler
{

    [SerializeField]
    private SpeedMultiplier speedMultiplier;

    private Scrollbar bar;

    // Use this for initialization
    void Awake()
    {
        bar = GetComponent<Scrollbar>();
    }

    public void OnDrag(PointerEventData data)
    {
        //print("_______________");
        //print("value: " + bar.value);
        speedMultiplier.SetSpeedMultiplierPercentage(bar.value);
    }
}
