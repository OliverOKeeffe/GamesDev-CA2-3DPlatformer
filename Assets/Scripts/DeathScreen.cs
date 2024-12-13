using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    // Method to restart the game (reload the stored level)
    public void RestartGame()
    {
        Debug.Log("Restart button clicked.");

        // Access the DeathHandler to call RestartGame
        DeathHandler deathHandler = FindObjectOfType<DeathHandler>();
        if (deathHandler != null)
        {
            Debug.Log($"levelToRestart in DeathHandler: {deathHandler}");
            deathHandler.RestartGame(); // Use the RestartGame method from DeathHandler
        }
        else
        {
            Debug.LogError("DeathHandler not found in the scene! Ensure it persists between scenes.");
        }
    }

    // Method to go back to the main menu
    public void GoToMainMenu()
    {
        Debug.Log("Go to Main Menu button clicked.");

        // Access the DeathHandler to clear data if necessary
        DeathHandler deathHandler = FindObjectOfType<DeathHandler>();
        if (deathHandler != null)
        {
            Debug.Log("Returning to Main Menu and resetting DeathHandler.");
            deathHandler.GoToMainMenu();
        }
        else
        {
            Debug.LogWarning("DeathHandler not found! Proceeding to Main Menu without resetting.");
            SceneManager.LoadScene("MainMenu"); // Fallback to ensure functionality
        }
    }
}
