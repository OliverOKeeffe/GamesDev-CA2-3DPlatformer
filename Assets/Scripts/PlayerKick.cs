using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    public Animator animator;
    public float kickRange = 1.5f;  // How far the kick can reach
    public LayerMask aiLayer;  // Layer to identify AI characters
    public float damage = 15f;  // Kick damage value

    private void Update()
    {
        // Detect the input for kicking (e.g., Right Mouse Button or another button)
        if (Input.GetKeyDown(KeyCode.E))  // Press E key to kick
        {
            Kick();
        }
    }

    void Kick()
    {
        // Trigger the kick animation
        animator.SetTrigger("Kick");

        // Cast a sphere to detect AI within kick range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * kickRange, kickRange, aiLayer);

        foreach (Collider hit in hitColliders)
        {
            // If an AI character is hit, apply damage
            if (hit.gameObject.CompareTag("AI"))
            {
                AIHealth aiHealth = hit.GetComponent<AIHealth>();
                if (aiHealth != null)
                {
                    aiHealth.TakeDamage(damage);
                }
            }
        }
    }

    // Draw the kick range in the editor for visualization
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * kickRange, kickRange);
    }
}
