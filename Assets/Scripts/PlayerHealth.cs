using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth, maxHealth, DamageAmmount;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        currentHealth -= DamageAmmount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        
    }
}
