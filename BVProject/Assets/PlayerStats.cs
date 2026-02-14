using UnityEngine;

public class PlayerStats : MonoBehaviour
{   
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;   
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);
        healthBar.SetHealth(currentHealth);
    }
}
