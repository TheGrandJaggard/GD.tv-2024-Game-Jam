using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    private float currentHealth;
    public Action death;
    public Action attacked;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        Debug.Log($"{name} took {damage}/{currentHealth} damge");

        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            Die();
        }
        attacked?.Invoke();
    }

    public float GetHealth() => currentHealth;

    private void Die()
    {
        death?.Invoke();
    }
}