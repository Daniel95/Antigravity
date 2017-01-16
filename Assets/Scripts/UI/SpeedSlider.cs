using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpeedSlider : MonoBehaviour, IDragHandler
{

    [SerializeField]
    private SpeedMultiplier speedMultiplier;

    private Scrollbar _bar;

    // Use this for initialization
    void Awake()
    {
        _bar = GetComponent<Scrollbar>();
    }

    public void OnDrag(PointerEventData data)
    {
        speedMultiplier.SetSpeedMultiplierPercentage(_bar.value);
    }
}
