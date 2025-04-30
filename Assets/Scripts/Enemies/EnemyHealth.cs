using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Configuration")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Optional UI")]
    public Image healthBar;    // Drag health bar UI image here if needed

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        Debug.Log($"[{name}] Received damage: {amount}");
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0f)
            Die();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void Die()
    {
        // Optional: Add death effects here
        // - Play death animation
        // - Spawn particles
        // - Drop items
        // - Play sound
        // Example:
        // var anim = GetComponent<Animator>();
        // if (anim != null)
        //     anim.SetTrigger("Die");
        
        Destroy(gameObject);
    }
}
