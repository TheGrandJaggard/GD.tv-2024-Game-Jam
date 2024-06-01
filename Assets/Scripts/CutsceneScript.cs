using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject scene1;
    [SerializeField] GameObject scene2;
    [SerializeField] GameObject scene2Wizard;
    [SerializeField] GameObject scene3;
    [SerializeField] GameObject scene3Wizard;
    [SerializeField] GameObject scene4;
    [SerializeField] GameObject scene5;
    [SerializeField] GameObject scene6;

    private void Start()
    {
        scene1.SetActive(false);
        scene2.SetActive(false);
        scene2Wizard.SetActive(false);
        scene3.SetActive(false);
        scene3Wizard.SetActive(false);
        scene4.SetActive(false);
        scene5.SetActive(false);
        scene6.SetActive(false);

        PlayScene1();
    }

    private void PlayScene1()
    {
        scene1.SetActive(true);
        scene1.transform.DOScale(1.5f, speed).onComplete += PlayScene2;
    }

    private void PlayScene2()
    {
        scene2Wizard.SetActive(true);
        scene2Wizard.transform.DOMove(new Vector3(-4f, -1.3f, -0.1f), speed * 1.5f).onComplete += PlayScene3;
    }

    private void PlayScene3()
    {
        scene1.SetActive(false);
        scene2Wizard.SetActive(false);

        scene3.SetActive(true);
        scene3Wizard.SetActive(true);
        scene3Wizard.transform.DOMove(new Vector3(-2.5f, 0f, -0.1f), speed).onComplete += PlayScene4;
    }

    private void PlayScene4()
    {
        scene3.SetActive(false);
        scene3Wizard.SetActive(false);

        scene4.SetActive(true);
        scene4.transform.DOScale(1f, speed).onComplete += PlayScene5;
    }

    private void PlayScene5()
    {
        scene4.SetActive(false);

        scene5.SetActive(true);
        scene5.transform.DOScale(1f, speed).onComplete += PlayScene6;
    }

    private void PlayScene6()
    {
        scene5.SetActive(false);

        scene6.SetActive(true);
        scene6.transform.DOMove(new Vector3(2f, 0f, 0f), speed)
            .SetEase(Ease.InCubic).onComplete += () => StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(0.2f * speed);
        SceneManager.LoadScene("GameScene");
    }
}
