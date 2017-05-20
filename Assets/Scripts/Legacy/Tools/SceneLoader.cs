using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNewSceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNewSceneNumber(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
