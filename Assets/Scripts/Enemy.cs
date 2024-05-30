using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(ColorFader))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] EnemyTargetType targetType;
    [SerializeField] float damage;
    [SerializeField] float knockback;
    private float attackCooldown;
    private bool haveBeenAttacked = false;
    private bool alive = true;


    private void Start()
    {
        GetComponent<Health>().death += Die;
        GetComponent<Health>().attacked += () => haveBeenAttacked = true;
    }

    private void Update()
    {
        if (alive)
        {
            Act();
            Move();
        }
    }

    private void Act()
    {
        
    }

    private void Move()
    {
        Vector3 targetVector = GetTarget();

        var targetDirection = targetVector.normalized;
        transform.position = transform.position + (targetDirection * Time.deltaTime * speed);

        GetComponent<ColorFader>().SetFacingRight(targetVector.x > 0f);
    }

    private Vector3 GetTarget()
    {
        if (targetType == EnemyTargetType.PlayerOnly)
        {
            return GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        }
        else if (targetType == EnemyTargetType.PlayerIfAttacked)
        {
            if (haveBeenAttacked)
            {
                return GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            }
            else
            {
                return Vector3.left;
            }
        }
        else if (targetType == EnemyTargetType.WallOnly)
        {
            return Vector3.left;
        }
        else // avoid player // TODO
        {
            return Vector3.left;
        }
    }

    private void Die()
    {
        alive = false;

        GetComponent<Collider2D>().enabled = false;

        transform.DORotate(new Vector3(0, 0, 90), 1f, RotateMode.Fast)
            .onComplete += () => Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().Damage(damage);
            GetComponent<Health>().Damage(damage);
            if (alive) { Knockback(2f); }
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            var deathDamage = GetComponent<Health>().GetHealth();
            other.gameObject.GetComponent<Health>().Damage(deathDamage);
            GetComponent<Health>().Damage(deathDamage);
        }
    }

    private void Knockback(float magnitude)
    {
        Vector3 targetVector = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;

        var targetDirection = -targetVector.normalized;

        GetComponent<Rigidbody2D>().AddForce(targetDirection * knockback, ForceMode2D.Impulse);
    }

    public void SetDamageMult(float damageMult)
    {
        damage *= damageMult;
    }
}
