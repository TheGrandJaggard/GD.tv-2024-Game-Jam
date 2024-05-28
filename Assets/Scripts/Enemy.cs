using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] EnemyTargetType targetType;
    [SerializeField] float damage;
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

        if (targetVector.x < -0.01f) { transform.rotation = new Quaternion(0f, 180f, 0f, 0f); }
        if (targetVector.x > 0.01f) { transform.rotation = new Quaternion(0f, 0f, 0f, 0f); }
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
        Debug.Log("I'm dying!");
        
        alive = false;
        GetComponent<Animator>().speed = 0;

        transform.DORotate(new Vector3(0, 0, 100), 1f, RotateMode.Fast).SetEase(Ease.InOutElastic)
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
        Debug.Log("Knocking myself back");
        Vector3 targetVector = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;

        var targetLocation = -targetVector.normalized * magnitude;
        transform.DOMove(targetLocation, 1f).SetEase(Ease.OutFlash);
    }
}
