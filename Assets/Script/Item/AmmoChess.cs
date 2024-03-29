using DG.Tweening;
using MarchingBytes;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AmmonChess : MonoBehaviour, IItem
{
    public Transform player;
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    void OnEnable()
    {
        StartCoroutine(Wait());
    }
    void OnDisable()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    IEnumerator Wait()
    {
        while (player == null)
        {
            yield return null;
        }
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance < 0.3f)
            {
                Pick();
                yield break;
            }
            if (time > 2.5f)
            {
                Pick();
                yield break;
            }
            yield return null;
        }
    }
    public void Increase()
    {
        gameObject.SetActive(false);
        int ammo = Random.Range(50, 80);
        Actions.PickAmmoChess?.Invoke(ammo);
    }
    public void Pick()
    {
        StartCoroutine(PickUp());
    }

    IEnumerator PickUp ()
    {
        while(Vector2.Distance(transform.position,player.position) > 0.02f) {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * 5 * Time.deltaTime);
            yield return null;
        }
        Increase();
        yield break;
    }
}
