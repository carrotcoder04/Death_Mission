using MarchingBytes;
using System.Collections;
using UnityEngine;
public class BulletController : Singleton<BulletController>
{
    public int indexgun = 0;
    public float maxfireTime = 0.3f;
    public float maxslashTime = 0.2f;
    private float firetime = 0;
    private float slashtime = 0;
    [SerializeField] Transform firepos;
    [SerializeField] Transform _firepos;
    [SerializeField] Transform playerpos;
    [SerializeField] GameObject bulletparent;
    [SerializeField] GameObject slash;
    private void Update()
    {
        firetime += Time.deltaTime;
        slashtime += Time.deltaTime;
        if (Input.GetMouseButton(0) && firetime > maxfireTime)
        {
            StartCoroutine(Fire());
            firetime = 0;
        }
        if (Input.GetMouseButton(1) && slashtime > maxslashTime)
        {
            StartCoroutine(Slash());
            slashtime = 0;
        }
    }
    IEnumerator Fire()
    {
        bulletparent.SetActive(true);
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        Vector3 direct = (target - _firepos.transform.position).normalized;
        Vector3 destination = playerpos.transform.position;
        GameObject bulletsprite = EasyObjectPool.instance.GetObjectFromPool("BulletSprite", destination, _firepos.rotation);
        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(0.2f, 0.25f);
        destination = destination + new Vector3(x,y, 0);
        StartCoroutine(Move(bulletsprite, destination));
        GameObject bullettmp = EasyObjectPool.instance.GetObjectFromPool("BulletRevolve", firepos.transform.position, _firepos.rotation);
        Rigidbody2D rb = bullettmp.GetComponent<Rigidbody2D>();
        rb.AddForce(direct * 5f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        bulletparent.SetActive(false);
        if (bullettmp != null)
        {
            bullettmp.SetActive(false);
            EasyObjectPool.instance.ReturnObjectToPool(bullettmp);
        }
    }
    IEnumerator Slash()
    {
        slash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        slash.SetActive(false);
    }
    IEnumerator Move(GameObject bullet,Vector3 destination)
    {
        while (Vector3.Distance(bullet.transform.position, destination) > 0.01f)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, destination,1.5f*Time.deltaTime);
            yield return null;
        }
    }
}
