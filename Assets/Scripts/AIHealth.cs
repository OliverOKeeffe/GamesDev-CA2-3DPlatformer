using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float health = 100f;

    // Call this method when the AI gets hit
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle the death of the AI (e.g., play death animation, disable the AI)
        Debug.Log("AI has died!");
        Destroy(gameObject);  // Or replace this with your own death logic
    }
}
