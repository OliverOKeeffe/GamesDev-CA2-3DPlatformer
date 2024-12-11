using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    // Variable to hold the current level name
    private string levelToRestart;

    // This function is called when the player dies, passing the current level name
    public void ShowDeathScreen(string levelName)
    {
        levelToRestart = levelName;  // Store the level name when the death screen is triggered
        Debug.Log("Death screen shown for level: " + levelName);  // Debug log to confirm the level name is being passed

        // Load the DeathScreen scene
        SceneManager.LoadScene("DeathScreen");  // Ensure that the scene name matches exactly
        Debug.Log("Transitioning to DeathScreen...");  // Confirm scene transition
    }

    // Method to restart the game (reload the level that was passed)
    public void RestartGame()
    {
        if (!string.IsNullOrEmpty(levelToRestart))
        {
            Debug.Log("Restarting level: " + levelToRestart);  // Debug log to confirm the level is restarting
            SceneManager.LoadScene(levelToRestart);
        }
    }

    // Method to go back to the main menu (or any other scene)
    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        SceneManager.LoadScene("MainMenu");  // Ensure "MainMenu" is the name of your main menu scene
    }
}
