using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class HeathBar : Singleton<HeathBar>
{
    [SerializeField] List<Sprite> hud;
    [SerializeField] List<Sprite> hp;
    [SerializeField] Player player;
    [SerializeField] SpriteRenderer hudRenderer,hpRenderer;
    private void OnEnable()
    {
        StartCoroutine(HpUpdate());
    }
    private void Start()
    {
        Actions.HPChange += OnHPChange;
    }
    private void OnHPChange()
    {
        float percent = player.currentHp / player.maxHp;
        if(percent <= 0)
        {
            hpRenderer.sprite = hp[0];
        }
        else if(percent > 0  && percent <= 1.0f/ 4)
        {
            hpRenderer.sprite = hp[1];
        }
        else if (percent > 1.0f/4 && percent <= 2.0f / 4)
        {
            hpRenderer.sprite = hp[2];
        }
        else if (percent > 2.0f / 4 && percent <= 3.0f / 4)
        {
            hpRenderer.sprite = hp[3];
        }
        else if (percent > 3.0f / 4)
        {
            hpRenderer.sprite = hp[4];
        }
    }
    IEnumerator HpUpdate()
    {
        int step = 6;
        while(player.currentHp > 0)
        {
            if(player.currentHp < player.maxHp)
            {
                if(step == 6)
                {
                    Heal();
                    step = 7;
                }
            }
            if (step == 7)
            {
                step = 0;
            }
            else if(step < 6)
            {
                step++;
            }
            hudRenderer.sprite = hud[step];
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }
    public void Heal()
    {
        float newHp = player.currentHp + player.maxHp * 1.0f / 6;
        if (newHp < player.maxHp)
        {
            player.currentHp = newHp;
        }
        else
        {
            player.currentHp = player.maxHp;
        }
        OnHPChange();
    }
}
