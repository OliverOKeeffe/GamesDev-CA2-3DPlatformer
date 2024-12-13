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
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes
        Debug.Log("DeathHandler initialized and persisted.");
    }


    public void RestartGame()
    {
        if (string.IsNullOrEmpty(levelToRestart))
        {
            Debug.LogError("No levelToRestart set! Cannot restart. Falling back to main menu.");
            SceneManager.LoadScene("MainMenu"); // Fallback to main menu or another scene
            return;
        }

        Debug.Log($"Restarting level: {levelToRestart}");
        SceneManager.LoadScene(levelToRestart);
    }


    public void ShowDeathScreen(string levelName)
    {
        if (!string.IsNullOrEmpty(levelName))
        {
            levelToRestart = levelName; // Set the level to restart
            Debug.Log($"Death screen shown for level: {levelName}. levelToRestart set to: {levelToRestart}");
            SceneManager.LoadScene("DeathScreen");
        }
        else
        {
            Debug.LogError("Level name is null or empty! Cannot show death screen.");
        }
    }


    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        SceneManager.LoadScene("MainMenu");
    }

    // New method for setting levelToRestart
    public void SetLevelToRestart(string levelName)
    {
        if (string.IsNullOrEmpty(levelName))
        {
            Debug.LogError("Cannot set levelToRestart to null or empty!");
        }
        else
        {
            levelToRestart = levelName;
            Debug.Log($"levelToRestart set to: {levelName}");
        }
    }

    void OnEnable()
    {
        Debug.Log($"DeathHandler active in scene: {SceneManager.GetActiveScene().name}");
        Debug.Log($"Current levelToRestart: {levelToRestart}");
    }
}
