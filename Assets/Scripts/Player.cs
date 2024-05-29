using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;
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
            GetComponent<Animator>().SetTrigger("Attack");
            attackCooldown = 0.3f;
            // Hit();
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

        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(hMovement) + Mathf.Abs(vMovement));
        if (hMovement < -0.01f) { transform.rotation = new Quaternion(0f, 180f, 0f, 0f); }
        if (hMovement > 0.01f)  { transform.rotation = new Quaternion(0f, 0f, 0f, 0f); }
    }

    private void Die()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }

    private void Hit() // called by animator
    {
        var directionalOffset = Vector3.up * GetComponent<CapsuleCollider2D>().offset.y * transform.localScale.y
            + (transform.rotation == new Quaternion() ? Vector3.left : Vector3.right);
        Debug.Log($"transform.position + directionalOffset = {transform.position + directionalOffset}");
        foreach (var other in Physics2D.OverlapCircleAll(transform.position + directionalOffset, 1f))
        {
            Debug.Log($"Hit {other.name}");
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Health>().Damage(damage);
                Debug.Log($"Damaged {other.name} with sword");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        var directionalOffset = Vector3.up * GetComponent<CapsuleCollider2D>().offset.y * transform.localScale.y
            + (transform.rotation == new Quaternion() ? Vector3.left : Vector3.right);
        Gizmos.DrawWireSphere(transform.position + directionalOffset, 1f);
    }
}
