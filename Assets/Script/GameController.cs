using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] List<GameObject> sceen = new List<GameObject>();
    [SerializeField] GameObject playerGO;
    [SerializeField] Transform cam;
    [SerializeField] GameObject loading;
    [SerializeField] CameraFollower camfollow;
    [SerializeField] CanvasGroup fade;
    private int currentSceen = 0;
    private void Start()
    {
        FadeOut();
    }
    public void LoadSceen(int id)
    {
        StartCoroutine(Loading(id));
    }
    public void Play()
    {
        LoadSceen(0);
    }
    public void FadeIn()
    {
        fade.alpha = 0f;
        fade.DOFade(1, 1);
    }
    public void FadeOut()
    {
        fade.alpha = 1f;
        fade.DOFade(0, 1);
    }
    IEnumerator Loading(int id)
    {
        FadeIn();
        yield return new WaitForSeconds(1f);
        int lastsceen = currentSceen;
        currentSceen = id;
        fade.alpha = 0;
        lock (cam)
        {
            lock(playerGO.transform)
            {
                loading.SetActive(true);
                playerGO.transform.position = sceen[id].transform.position;
                cam.transform.position = sceen[id].transform.position + new Vector3(0,0,-10);
                yield return new WaitForSeconds(0.5f);
                loading.SetActive(false);
                sceen[id].SetActive(true);
            }
        }
        fade.alpha = 1;
        FadeOut();
        yield return new WaitForSeconds(1f);
        yield break;
    }
}
