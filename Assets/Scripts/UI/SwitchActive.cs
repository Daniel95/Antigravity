using UnityEngine;

public class SwitchActive : MonoBehaviour
{
    [SerializeField]
    private Element[] elements;

    public void SwitchCurrentState()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i].gameObject.activeSelf)
                elements[i].gameObject.SetActive(false);
            else
                elements[i].gameObject.SetActive(true);
        }
    }

    public void SwitchToGivenState() {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].gameObject.SetActive(elements[i].nextState);
            elements[i].nextState = !elements[i].nextState;
        }
    }

    [System.Serializable]
    private struct Element {
        public GameObject gameObject;
        public bool nextState;
    }
}
