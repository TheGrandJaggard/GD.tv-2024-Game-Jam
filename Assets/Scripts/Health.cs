
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    private float currentHealth;
    public UnityEvent death;

    private void Start()
    {
        currentHealth = health;
    }

    public void Damage(float damage)
    {
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