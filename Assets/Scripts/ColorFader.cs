using UnityEngine;

public class ColorFader : MonoBehaviour
{
    [SerializeField] SpriteRenderer fullHealthSprite;
    [SerializeField] SpriteRenderer noHealthSprite;

    public void Start()
    {
        noHealthSprite.transform.position = new Vector3
        (
            fullHealthSprite.transform.position.x,
            fullHealthSprite.transform.position.y,
            1
        );
    }

    public void SetColor(float decimalFade)
    {
        fullHealthSprite.color = new Color(1f, 1f, 1f, decimalFade);
    }

    public void SetFacingRight(bool facingRight)
    {
        fullHealthSprite.flipX = !facingRight;
        noHealthSprite.flipX = !facingRight;
    }

    public bool IsFacingRight()
    {
        return fullHealthSprite.flipX;
    }

    public void SetAnimTrigger(string name)
    {
        fullHealthSprite.GetComponent<Animator>().SetTrigger(name);
        noHealthSprite.GetComponent<Animator>().SetTrigger(name);
    }

    public void SetAnimBool(string name, bool value)
    {
        fullHealthSprite.GetComponent<Animator>().SetBool(name, value);
        noHealthSprite.GetComponent<Animator>().SetBool(name, value);
    }
    
    public void SetAnimFloat(string name, float value)
    {
        fullHealthSprite.GetComponent<Animator>().SetFloat(name, value);
        noHealthSprite.GetComponent<Animator>().SetFloat(name, value);
    }
}
