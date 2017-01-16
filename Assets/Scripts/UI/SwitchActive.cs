using UnityEngine;

public class SwitchActive : MonoBehaviour, ITriggerable
{

    public bool Triggered { get; set; }

    [SerializeField]
    private Element[] elements;

    public void SwitchCurrentState()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            //switch the current state
            elements[i].gameObject.SetActive(!elements[i].gameObject.activeSelf);

            //then assing the nextState as the opposite of the new currenState
            elements[i].nextState = !elements[i].gameObject.activeSelf;
        }
    }

    public void SwitchToNextState() {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].gameObject.SetActive(elements[i].nextState);

            elements[i].nextState = !elements[i].gameObject.activeSelf;
        }
    }

    public void SwitchToState(bool _state)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].gameObject.SetActive(_state);

            elements[i].nextState = !elements[i].gameObject.activeSelf;
        }
    }

    [System.Serializable]
    private struct Element {
        public GameObject gameObject;
        public bool nextState;
    }

    public void TriggerActivate()
    {
        SwitchToNextState();
    }

    public void TriggerStop()
    {
        SwitchToNextState();
    }
}
