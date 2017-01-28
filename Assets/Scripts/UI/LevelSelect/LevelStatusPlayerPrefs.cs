using UnityEngine;

public class LevelStatusPlayerPrefs : MonoBehaviour {

    public static void SetLevelStatus(int levelNumber, int status)
    {
        PlayerPrefs.SetInt(levelNumber.ToString(), status);
    }

    public static void SaveLevelStatuses()
    {
        PlayerPrefs.Save();
    }

    public static int GetLevelStatus(int levelNumber)
    {
        if (PlayerPrefs.HasKey(levelNumber.ToString()))
            return PlayerPrefs.GetInt(levelNumber.ToString());

        return 0;
    }

    public static void DeleteAllSavedData()
    {
        PlayerPrefs.DeleteAll();
    }
}

