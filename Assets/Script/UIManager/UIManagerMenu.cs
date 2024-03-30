using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerMenu : MonoBehaviour
{
    [SerializeField] Button play;
    void Start()
    {
        play.onClick.AddListener(() => Play());
    }

   public void Play()
    {
        SceenMenu.Instance.LoadGame();
    }
}
