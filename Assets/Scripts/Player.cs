using UnityEngine;

[RequireComponent(typeof(ColorFader))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float range;
    private float attackCooldown;

    private void Start()
    {
        GetComponent<Health>().death += Die;
    }

    private void Update()
    {
        Act();
        Move();
    }

    private void Act()
    {
        attackCooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && attackCooldown < 0)
        {
            GetComponent<ColorFader>().SetAnimTrigger("Attack");
            attackCooldown = 0.3f;
        }
        if (Input.GetButton("Fire2"))
        {
            // special (gun or trap)
            // right now this is just a health / damage test
            GetComponent<Health>().Damage(40f);
        }
    }

    private void Move()
    {
        var hMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var vMovement = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.position = transform.position + new Vector3(hMovement, vMovement, 0f);

        GetComponent<ColorFader>().SetAnimFloat("Speed", Mathf.Abs(hMovement) + Mathf.Abs(vMovement));
        
        if (hMovement > 0.01f || hMovement < -0.01f)
        {
            GetComponent<ColorFader>().SetFacingRight(hMovement > 0f);
        }
    }

    private void Die()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }

    public void Hit() // called by animator relay
    {
        // Debug.Log("Hit");
        var directionalOffset = Vector3.up * GetComponent<CapsuleCollider2D>().offset.y * transform.localScale.y
            + (GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right) * range;
        
        foreach (var other in Physics2D.OverlapCircleAll(transform.position + directionalOffset, range))
        {
            Debug.Log($"Hit {other.name}");
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Health>().Damage(damage);
                // Debug.Log($"Damaged {other.name} with sword");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        var directionalOffset = Vector3.up * GetComponent<CapsuleCollider2D>().offset.y * transform.localScale.y
            + (GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right) * range;
        Gizmos.DrawWireSphere(transform.position + directionalOffset, range);
    }
}
