using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SceenMenu : Singleton<SceenMenu>
{
    [SerializeField] CanvasGroup fade;
    [SerializeField] GameObject loading;
    void Start()
    {
        FadeOut();
    }
    public void FadeIn()
    {
        fade.alpha = 0f;
        fade.DOFade(1, 1);
    }
    public void FadeOut()
    {
        fade.alpha = 1f;
        fade.DOFade(0, 1).OnComplete(() => fade.gameObject.SetActive(false));
    }
    public void LoadGame()
    {
        fade.gameObject.SetActive(true);
        StartCoroutine(Play());
    }
    IEnumerator Play()
    {
        FadeIn();
        yield return new WaitForSeconds(1f);
        loading.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
