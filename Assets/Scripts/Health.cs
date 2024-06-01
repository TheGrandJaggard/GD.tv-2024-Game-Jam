using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] AudioClip damagedClip;
    private float currentHealth;
    public Action death;
    public Action attacked;

    private void Start()
    {
        currentHealth = maxHealth;
        GetComponent<ColorFader>()?.SetColor(currentHealth / maxHealth);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.1f)
        {
            Die();
        }

        Debug.Log($"{name} took {damage} damage. {currentHealth} health left.");
        GetComponent<ColorFader>()?.SetColor(currentHealth / maxHealth);

        attacked?.Invoke();
        GetComponent<AudioSource>().PlayOneShot(damagedClip);
    }

    public float GetHealth() => currentHealth;

    private void Die()
    {
        death?.Invoke();
    }

    public void SetHealthMult(float healthMult)
    {
        maxHealth *= healthMult;
        currentHealth *= healthMult;
    }
}