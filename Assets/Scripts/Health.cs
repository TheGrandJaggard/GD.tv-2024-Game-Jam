using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    private float currentHealth;
    public Action death;

    private void Start()
    {
        currentHealth = health;
    }

    public void Damage(float damage)
    {
        Debug.Log($"{name} took {damage}/{currentHealth} damge");

        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        death?.Invoke();
    }
}