using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] List<GameObject> sceen = new List<GameObject>();
    [SerializeField] GameObject player;
    [SerializeField] Transform cam;
    [SerializeField] GameObject loading;
    [SerializeField] GameObject tittle;
    [SerializeField] GameObject background;
    [SerializeField] CameraFollower camfollow;
    [SerializeField] CanvasGroup fade;
    private int currentSceen = 3;
    private void Awake()
    {
        StartCoroutine(LoadTittleAndMenu());
    }
    public void LoadSceen(int id)
    {
        StartCoroutine(Loading(id));
    }
    public void Play()
    {
        background.SetActive(false);
        LoadSceen(0);
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
    private IEnumerator LoadTittleAndMenu()
    {
        cam.transform.position = sceen[3].transform.position + new Vector3(0,0,-10);
        tittle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        FadeIn();
        yield return new WaitForSeconds(1f);
        FadeOut();
        tittle.SetActive(false);
        sceen[3].SetActive(true);
        yield return new WaitForSeconds(1f);
        yield break;
    }
    IEnumerator Loading(int id)
    {
        FadeIn();
        yield return new WaitForSeconds(1f);
        int lastsceen = currentSceen;
        if (currentSceen == 3)
        {
            sceen[currentSceen].SetActive(false);
            camfollow.enabled = true;
            background.SetActive(false);
        }
        else
        {
            sceen[currentSceen].SetActive(false);
        }
        currentSceen = id;
        if(currentSceen == 3)
        {
            camfollow.enabled = false;
        }
        if (lastsceen == 3)
        {
            sceen[lastsceen].SetActive(false);
        }
        fade.alpha = 0;
        lock (cam)
        {
            lock(player.transform)
            {
                loading.SetActive(true);
                player.transform.position = sceen[id].transform.position;
                cam.transform.position = sceen[id].transform.position + new Vector3(0,0,-10);
                yield return new WaitForSeconds(0.5f);
                loading.SetActive(false);
                sceen[id].SetActive(true);
            }
        }
        if(currentSceen == 3)
        {
            background.SetActive(true);
        }
        fade.alpha = 1;
        FadeOut();
        yield return new WaitForSeconds(1f);
        yield break;
    }
}
