using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button start;
    void Start()
    {
        start.onClick.AddListener(() => Play());
    }
    void Play()
    {
        GameController.Instance.Play();
    }
}
