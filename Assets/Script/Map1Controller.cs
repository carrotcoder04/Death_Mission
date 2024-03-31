using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1Controller : Singleton<Map1Controller>
{
    [SerializeField] Transform rd1x,rd2x,rd3x,rd1y,rd2y,rd3y,rd4y;
    List<Bot> botlist = new List<Bot>();
    public int botcount;
    public int maxbot;
    public Transform playerTF;
    float x1;
    float x2;
    float x3;
    float y1;
    float y2;
    float y3;
    float y4;
    void Start()
    {
        Actions.killbot += KillBot;
        Invoke(nameof(OnInit),1f);
        x1 = rd1x.position.x;
        x2 = rd2x.position.x;
        x3 = rd3x.position.x;
        y1 = rd1y.position.y;
        y2 = rd2y.position.y;
        y3 = rd3y.position.y;
        y4 = rd4y.position.y;
    }
    void OnInit()
    {
        Born1(botcount/3);
        Born2(botcount - botcount/3);
    }
    void KillBot(Bot bot)
    {
        botlist.Remove(bot);
        if (botcount < maxbot)
        {
            Invoke(nameof(Born2), 5f);
        }
        else if (botcount >= maxbot)
        {

        }
    }
    void Born1(int botCount)
    {
        for (int i = 0; i < botCount; i++)
        {
            Vector2 rand = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
            GameObject botclone = EasyObjectPool.instance.GetObjectFromPool(Constants.ZOMBIE, rand, Quaternion.identity);
            Bot bot = botclone.GetComponent<Bot>();
            botlist.Add(bot);
        }
    }
    void Born2(int botCount)
    {
        for (int i = 0; i < botCount; i++)
        {
            Vector2 rand = new Vector2(Random.Range(x2, x3), Random.Range(y3, y4));
            GameObject botclone = EasyObjectPool.instance.GetObjectFromPool(Constants.ZOMBIE, rand, Quaternion.identity);
            Bot bot = botclone.GetComponent<Bot>();
            botlist.Add(bot);
        }
    }
    void Born2()
    {
        Vector2 rand = new Vector2(Random.Range(x2, x3), Random.Range(y3, y4));
        GameObject botclone = EasyObjectPool.instance.GetObjectFromPool(Constants.ZOMBIE, rand, Quaternion.identity);
        Bot bot = botclone.GetComponent<Bot>();
        botlist.Add(bot);
    }
    public void SetDistance(float distance)
    {
        foreach(var bot in botlist)
        {
            bot.distance = distance;
        }
    }
}
