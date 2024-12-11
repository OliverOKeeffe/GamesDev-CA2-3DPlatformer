using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class DeathScreen : MonoBehaviour
{
    // Method to restart the game (reload Level1)
    public void RestartGame()
    {
        // You can manually specify the level scene here
        SceneManager.LoadScene("Level1");  // Replace "Level1" with your level's scene name if it's different
    }

    // Method to go back to the main menu (or any other scene)
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Make sure "MainMenu" is the name of your main menu scene
    }
}
