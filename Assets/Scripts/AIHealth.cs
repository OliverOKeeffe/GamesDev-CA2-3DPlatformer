using UnityEngine;
using System.Collections;  

public class AIHealth : MonoBehaviour
{
    public float health = 100f;
    private Animator animator;
    public GameObject victoryPrefab;  
    public Transform spawnPoint;  

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0) return;  // Prevent multiple death triggers

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Trigger the death animation
        animator.SetTrigger("Die");

        // Disable AI components (NavMeshAgent, collider, etc.)
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Start a coroutine to wait for the death animation to finish and then spawn the prefab
        // StartCoroutine(WaitForDeathAnimation());
    }

    // IEnumerator WaitForDeathAnimation()
    // {
    // // Wait for the death animation to finish
    // yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

    // // Adjust the spawn position by adding an offset to make it above the ground or at a desired height
    // Vector3 spawnPosition = spawnPoint.position + new Vector3(0, 1, 0); // Adjust the Y value for height

    // // Instantiate the victory prefab at the adjusted spawn position
    // Instantiate(victoryPrefab, spawnPosition, Quaternion.identity);
    // }
}
