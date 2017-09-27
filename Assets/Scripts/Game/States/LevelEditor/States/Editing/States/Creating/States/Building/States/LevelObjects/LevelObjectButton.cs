using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelObjectButton : MonoBehaviour {

    [SerializeField] private Text text;

    private Button button;

    public void Initiate(LevelObjectType levelObjectType) {
        text.text = levelObjectType.ToString();
        button = GetComponent<Button>();
    }

}
