using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    public void HandlePlayerDeath()
    {
        DeathHandler deathHandler = FindObjectOfType<DeathHandler>();
        if (deathHandler != null)
        {
            string currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            Debug.Log($"Player died in level: {currentLevel}");
            deathHandler.ShowDeathScreen(currentLevel); // Pass the current level name
        }
        else
        {
            Debug.LogError("DeathHandler not found in the scene!");
        }
    }
}
