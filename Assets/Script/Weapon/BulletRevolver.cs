using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRevolver : MonoBehaviour
{
    [SerializeField] List<GameObject> trail = new List<GameObject>();
    BulletRevolverHit hit;
    private void Start()
    {
        hit = GetComponent<BulletRevolverHit>();
    }
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
        for(int i = trail.Count-1; i >= 0; i--)
        {
            if(hit.isHit)
            {
                DisAppear();
                yield break;
            }
            trail[i].SetActive(true);
            yield return new WaitForSeconds(0.02f);
        }
        yield break;
    }
    void DisAppear()
    {
        for (int i = 0; i < trail.Count; i++)
        {
            trail[i].SetActive(false);
        }
    }
}