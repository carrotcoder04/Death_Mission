using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceenStart : MonoBehaviour
{
    [SerializeField] CanvasGroup fade;
    void Start()
    {
        StartCoroutine(LoadSceenMenu());   
    }
    IEnumerator LoadSceenMenu()
    {
        yield return new WaitForSeconds(1f);
        FadeIn();
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void FadeIn()
    {
        fade.alpha = 0f;
        fade.DOFade(1, 1);
    }
    public void FadeOut()
    {
        fade.DOFade(0, 1);
    }
}
