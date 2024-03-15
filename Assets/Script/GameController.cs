using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] List<GameObject> sceen = new List<GameObject>();
    [SerializeField] GameObject player;
    [SerializeField] Transform cam;
    [SerializeField] GameObject loading;
    [SerializeField] GameObject tittle;
    [SerializeField] GameObject fade;
    [SerializeField] GameObject background;
    [SerializeField] Animator animatorFade;
    [SerializeField] CameraFollower camfollow;
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
    
    private IEnumerator LoadTittleAndMenu()
    {
        cam.transform.position = sceen[3].transform.position + new Vector3(0,0,-10);
        tittle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fade.SetActive(true);
        yield return new WaitForSeconds(1f);
        tittle.SetActive(false);
        animatorFade.SetTrigger("End");
        sceen[3].SetActive(true);
        yield return new WaitForSeconds(1f);
        fade.SetActive(false);
    }
    IEnumerator Loading(int id)
    {
        int lastsceen = currentSceen;
        if (currentSceen == 3)
        {
            sceen[currentSceen].SetActive(false);
            camfollow.enabled = true;
            yield return new WaitForSeconds(0.5f);
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
    }
}
