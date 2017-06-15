using UnityEngine;

public class LevelStatusAppearances : MonoBehaviour {

    [SerializeField] private Color[] levelStatusColors;

    public static Color[] LevelStatusColors;

    public void Awake() {
        LevelStatusColors = levelStatusColors;
    }
}
