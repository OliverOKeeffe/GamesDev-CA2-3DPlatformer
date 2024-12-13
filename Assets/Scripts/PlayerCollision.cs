using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public float respawnTime = 0.5f;  // Delay before switching to the death screen
    private DeathHandler deathHandler;  // Reference to the DeathHandler script
    private string currentLevel;

    void Start()
    {
        // Find the DeathHandler script in the scene
        deathHandler = FindObjectOfType<DeathHandler>();
        if (deathHandler == null)
        {
            Debug.LogError("DeathHandler script not found in the scene.");
        }

        // Store the current scene (level) name
        currentLevel = SceneManager.GetActiveScene().name;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debugging: Log the collision details
        Debug.Log("Collision Detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Mountain"))
        {
            // Debugging: Log the event
            Debug.Log("Player hit the Mountain! Triggering death...");

            // Trigger death after respawnTime delay
            Invoke("TriggerDeath", respawnTime);
        }
    }

    private void TriggerDeath()
    {
        if (deathHandler != null)
        {
            string currentLevel = SceneManager.GetActiveScene().name; // Get the current level name
            Debug.Log("Passing current level to DeathHandler: " + currentLevel); // Log the current level name
            deathHandler.ShowDeathScreen(currentLevel); // Pass it to DeathHandler
        }
        else
        {
            Debug.LogError("DeathHandler is null!");
        }
        Debug.Log("Triggering death in level: " + currentLevel);

    }

}
