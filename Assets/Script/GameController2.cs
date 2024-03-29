using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController2 : Singleton<GameController2>
{
    public int totalgold { get; set; }
    void Start()
    {
        totalgold = 0;
        Actions.OnChangeGold?.Invoke(0);
        Actions.PickLifeChess += IncreaseHP;
        Actions.PickGoldChess += IncreaseGold;
        Actions.PickAmmoChess += IncreaseAmmo;
        Actions.killbot += KillBot;
    }

    private void IncreaseAmmo(int ammo)
    {
        int rand = Random.Range(0, 5);
        UIManager.Instance.IncreaseAmmmo(ammo, rand);
    }

    void IncreaseHP()
    {
        HeathBar.Instance.Heal();
    }
    void IncreaseGold(int gold)
    {
        Actions.OnChangeGold?.Invoke(gold);
    }
    void KillBot(Bot bot)
    {
        int rand = Random.Range(0, 12);
        if(rand == 0)
        {
            EasyObjectPool.instance.GetObjectFromPool(Constants.AMMMOCHESS,bot.transform.position,Quaternion.identity);
        }
        else if (rand == 1)
        {
            EasyObjectPool.instance.GetObjectFromPool(Constants.LIFECHESS, bot.transform.position, Quaternion.identity);
        }
        else if (rand == 2)
        {
            EasyObjectPool.instance.GetObjectFromPool(Constants.GOLDCHESS, bot.transform.position, Quaternion.identity);
        }
    }
}
