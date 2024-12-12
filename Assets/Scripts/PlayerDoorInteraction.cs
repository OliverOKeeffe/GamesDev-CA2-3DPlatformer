using UnityEngine;
using UnityEngine.SceneManagement; // For scene loading

public class PlayerDoorInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player touches the dungeon door
        if (other.CompareTag("DungeonDoor"))
        {
            Debug.Log("Player touched the Dungeon Door! Loading Level 2...");
            SceneManager.LoadScene("Level2-oliver"); // Replace with your actual Level 2 scene name
        }
    }
}
