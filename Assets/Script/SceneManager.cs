using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    private void OnEnable()
    {
        try
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].SetActive(true);
            }
        }
        catch { }
    }
    private void OnDisable()
    {
        try
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].SetActive(false);
            }
        }
        catch { }
    }
}
