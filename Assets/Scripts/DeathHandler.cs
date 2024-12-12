using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private static DeathHandler instance; // Singleton to persist data
    private string levelToRestart;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes
    }

    public void ShowDeathScreen(string levelName)
    {
        levelToRestart = levelName;
        Debug.Log("Death screen shown for level: " + levelName);
        SceneManager.LoadScene("DeathScreen");
    }

    public void RestartGame()
    {
        if (!string.IsNullOrEmpty(levelToRestart))
        {
            Debug.Log("Restarting level: " + levelToRestart);
            SceneManager.LoadScene(levelToRestart);
        }
    }

    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
}
