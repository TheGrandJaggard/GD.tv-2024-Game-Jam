using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CutsceneScene");
    }

    public void SkipCutscene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowHighscores()
    {

    }
}
