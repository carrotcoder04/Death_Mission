using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRevolver : MonoBehaviour
{
    [SerializeField] List<GameObject> trail = new List<GameObject>();
    private void OnEnable()
    {
        StartCoroutine(Appear());
    }
    private void OnDisable()
    {
        for(int i = 0; i < trail.Count; i++)
        {
            trail[i].SetActive(false);
        }
    }
    IEnumerator Appear()
    {
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i < trail.Count; i++)
        {
            trail[i].SetActive(true);
        }
    }
}