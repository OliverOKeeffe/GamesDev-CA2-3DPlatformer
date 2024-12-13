using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    private static DeathHandler instance; // Singleton instance
    private string levelToRestart; // Stores the name of the level to restart

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Duplicate DeathHandler detected. Destroying duplicate.");
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes
    }

    public void RestartGame()
    {
        if (!string.IsNullOrEmpty(levelToRestart))
        {
            Debug.Log($"Restarting level: {levelToRestart}");
            SceneManager.LoadScene(levelToRestart);
        }
        else
        {
            Debug.LogError("No levelToRestart set! Cannot restart.");
        }
    }

    public void ShowDeathScreen(string levelName)
    {
        if (string.IsNullOrEmpty(levelName))
        {
            Debug.LogError("Level name is null or empty! Cannot show death screen.");
            return;
        }

        levelToRestart = levelName;
        Debug.Log($"Death screen shown for level: {levelName}");
        SceneManager.LoadScene("DeathScreen");
    }

    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        SceneManager.LoadScene("MainMenu");
    }

    // For debugging purposes: Check persistence and the current state of DeathHandler
    void OnEnable()
    {
        Debug.Log($"DeathHandler active in scene: {SceneManager.GetActiveScene().name}");
        Debug.Log($"Current levelToRestart: {levelToRestart}");
    }
}
