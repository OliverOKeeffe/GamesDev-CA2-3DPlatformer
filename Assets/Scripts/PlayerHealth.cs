using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth, maxHealth, DamageAmmount;
    public Healthbar healthbar;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar?.SetMaxHealth(currentHealth); // Initialize health bar
    }

    public void DealDamage()
    {
        currentHealth -= DamageAmmount;
        Debug.Log("Damage dealt. Current health: " + currentHealth);

        healthbar?.SetHealth(currentHealth); // Update health bar

        if (currentHealth <= 0)
        {
            ResetScene();
        }
    }

    private void ResetScene()
    {
        Debug.Log("Player health reached zero. Resetting scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
