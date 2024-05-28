using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        Act();
        Move();
    }

    private void Act()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // attack
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // special (gun or trap)
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
}
