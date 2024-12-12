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
            deathHandler.RestartGame(); // Use the RestartGame method from DeathHandler
        }
        else
        {
            Debug.LogError("DeathHandler not found in the scene!");
        }
    }

    // Method to go back to the main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Make sure "MainMenu" is the name of your main menu scene
    }
}
