using UnityEngine;

public class SwitchActive : MonoBehaviour
{
    [SerializeField]
    private GameObject element;

    public void Switch()
    {
        if (element.activeSelf)
            element.SetActive(false);
        else
            element.SetActive(true);
    }
}
