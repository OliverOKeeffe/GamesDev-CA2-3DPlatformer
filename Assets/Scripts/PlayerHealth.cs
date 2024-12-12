using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth, maxHealth, DamageAmmount;
    public Healthbar healthbar;

    private string currentLevel;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar?.SetMaxHealth(currentHealth); // Initialize health bar

        // Store the current scene name
        currentLevel = SceneManager.GetActiveScene().name;
    }

    public void DealDamage()
    {
        currentHealth -= DamageAmmount;
        Debug.Log("Damage dealt. Current health: " + currentHealth);

        healthbar?.SetHealth(currentHealth); // Update health bar

        if (currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        Debug.Log("Player health reached zero. Loading death screen...");
        PlayerPrefs.SetString("LevelToRestart", currentLevel); // Store the level in PlayerPrefs
        SceneManager.LoadScene("DeathScreen"); // Replace with the actual name of your death screen scene
    }
}
