using UnityEngine;

[RequireComponent(typeof(Health))]
public class Wall : MonoBehaviour
{
    void Start()
    {
        GetComponent<Health>().death += Die;
    }

    private void Die()
    {
        GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<LevelManager>()
            .GameOver();
    }
}
