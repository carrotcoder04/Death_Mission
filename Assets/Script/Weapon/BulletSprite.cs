using MarchingBytes;
using System.Collections;
using UnityEngine;

public class BulletSprite : MonoBehaviour
{
    [SerializeField] Sprite none, bullet;
    [SerializeField] SpriteRenderer render;
    private void OnEnable()
    {
        StartCoroutine(ChangeSprite());
    }
    private void OnDisable()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    IEnumerator ChangeSprite()
    {
        yield return new WaitForSeconds(1f);
        render.sprite = none;
        yield return new WaitForSeconds(0.1f);
        render.sprite = bullet;
        yield return new WaitForSeconds(0.1f);
        render.sprite = none;
        yield return new WaitForSeconds(0.1f);
        render.sprite = bullet;
        yield return new WaitForSeconds(0.1f);
        render.sprite = none;
        yield return new WaitForSeconds(0.1f);
        render.sprite = bullet;
        yield return new WaitForSeconds(0.1f);
        render.sprite = none;
        yield return new WaitForSeconds(0.1f);
        render.sprite = bullet;
        gameObject.SetActive(false);
    }
}
