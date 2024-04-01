using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject win;
    [SerializeField] private TextMeshProUGUI totalzombie, totaltime, totalgold;
    [SerializeField] private Button replay, nextgame, menu;
    string currentAnim;
    void Start()
    {
        Actions.GoIntotTheGate += GoIntoTheGate;
    }
    private void GoIntoTheGate()
    {
        StartCoroutine(OnWinAction(Map1Controller.Instance.maxbot,UIManager.Instance.timeTxt.text,GameController2.Instance.totalgold));
    }
    IEnumerator OnWinAction(int totalzombie, string totaltime, int totalgold)
    {
        win.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ChangeAnim("Win");
        this.totalzombie.text = totalzombie.ToString();
        this.totaltime.text = totaltime;
        this.totalgold.text = totalgold.ToString();
        yield break;
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (currentAnim != null) animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
