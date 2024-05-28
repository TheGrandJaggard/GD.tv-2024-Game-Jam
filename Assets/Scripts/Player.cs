using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed;
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
        if (Input.GetButtonDown("Fire1") && attackCooldown < 0)
        {
            GetComponent<Animator>().SetTrigger("Attack");
            attackCooldown = 0.6f;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            // special (gun or trap)
            GetComponent<Health>().Damage(120f);
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
        Debug.Log("I'm dying!");
    }
}
