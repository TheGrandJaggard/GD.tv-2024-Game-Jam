using UnityEngine;

public class AnimatorHitRelay : MonoBehaviour
{
    [SerializeField] bool relayOn;
    public void Hit()
    {
        if (relayOn)
        {
            GetComponentInParent<Player>().Hit();
        }
    }
}
