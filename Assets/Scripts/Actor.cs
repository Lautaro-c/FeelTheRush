using System;
using UnityEngine;

public class Actor : MonoBehaviour
{//
    int currentHealth;
    public int maxHealth;
    public event Action OnDie;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        ScoreManager.Instance?.RegisterKill();
        OnDie.Invoke();
    }
}