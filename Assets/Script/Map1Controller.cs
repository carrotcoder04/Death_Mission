using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1Controller : Singleton<Map1Controller>
{
    [SerializeField] Transform rd1x,rd2x,rd3x,rd1y,rd2y,rd3y,rd4y;
    List<Bot> botlist = new List<Bot>();
    public int botcount;
    public Transform playerTF;
    void Start()
    {
        Actions.killbot += KillBot;
        Invoke(nameof(OnInit), 2f);
    }
    void OnInit()
    {
        float x1 = rd1x.position.x;
        float x2 = rd2x.position.x;
        float x3 = rd3x.position.x;
        float y1 = rd1y.position.y;
        float y2 = rd2y.position.y;
        float y3 = rd3y.position.y;
        float y4 = rd4y.position.y;
        for (int i = 0; i < botcount - botcount / 3; i++)
        {
            Vector2 rand = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
            GameObject botclone = EasyObjectPool.instance.GetObjectFromPool("Zombie", rand, Quaternion.identity);
            Bot bot = botclone.GetComponent<Bot>();
            botlist.Add(bot);
        }
        for (int i = botcount - botcount / 3; i < botcount; i++)
        {
            Vector2 rand = new Vector2(Random.Range(x2, x3), Random.Range(y3, y4));
            GameObject botclone = EasyObjectPool.instance.GetObjectFromPool(Constants.ZOMBIE, rand, Quaternion.identity);
            Bot bot = botclone.GetComponent<Bot>();
            botlist.Add(bot);
        }
    }
    void KillBot(Bot bot)
    {
        botlist.Remove(bot);
    }
}
