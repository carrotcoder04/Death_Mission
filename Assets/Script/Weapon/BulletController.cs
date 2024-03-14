using MarchingBytes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
public class BulletController : Singleton<BulletController>
{
    public int indexgun = 0;
    public float maxslashTime = 0.1f;
    private float firetime = 0;
    private float slashtime = 0;
    private readonly List<string> listWeapon = new List<string>() { Constants.BULLETREVOLVER, Constants.BULLETREVOLVER, Constants.BULLETSHOTGUN, Constants.BULLETSHOTGUN, Constants.BULLETSHOTGUN};
    private readonly List<string> listSprite = new List<string>() { Constants.BULLETREVOLVERSPRTE, Constants.BULLETREVOLVERSPRTE, Constants.BULLETSHOTGUNSPRITE, Constants.BULLETSHOTGUNSPRITE, Constants.BULLETSHOTGUNSPRITE};
    private readonly List<int> numberofBullets = new List<int>() { 1, 2, 5, 1, 1 };
    private readonly List<int> deflection = new List<int>() { 0, 3, 10, 1, 1 };
    private readonly List<float> maxfireTime = new List<float> { 0.2f , 0.35f , 0.75f , 0.2f , 0.2f};
    private readonly List<float> listForce = new List<float> { 9f, 6f, 5f, 5f, 5f };
    private readonly List<float> flyTime = new List<float> { 0.5f, 0.5f, 0.18f, 0.4f, 0.4f };
    [SerializeField] Transform firepos;
    [SerializeField] Transform _firepos;
    [SerializeField] Transform playerpos;
    [SerializeField] GameObject bulletparent;
    [SerializeField] GameObject slash;
    private void Update()
    {
        firetime += Time.deltaTime;
        slashtime += Time.deltaTime;
        
        if (Input.GetMouseButton(0) && firetime > maxfireTime[indexgun])
        {
            StartCoroutine(Fire());
            
            firetime = 0;
        }
        if (Input.GetMouseButtonDown(1) && slashtime > maxslashTime)
        {
            StartCoroutine(Slash());
            CameraFollower.Instance.CameraShake(0.0005f, 0.0005f);
            slashtime = 0;
        }
    }
    IEnumerator Fire()
    {
        bool disappear = false;
        if (indexgun != 0 && indexgun != 1)
        {
            CameraFollower.Instance.CameraShake(0.003f, 0.003f);
            disappear = true;
        }
        else
        {
            CameraFollower.Instance.CameraShake(0.0005f, 0.0005f);
        }
        bulletparent.SetActive(true);
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        Vector3 direct = (target - _firepos.transform.position).normalized;
        List<GameObject> bullet = new List<GameObject>();
        List<GameObject> sprite = new List<GameObject>();
        List<Rigidbody2D> rb = new List<Rigidbody2D>();
        for (int i = 0; i < numberofBullets[indexgun]; i++)
        {
            GameObject bulletsprite = EasyObjectPool.instance.GetObjectFromPool(listSprite[indexgun], playerpos.position, _firepos.rotation);
            sprite.Add(bulletsprite);
            GameObject bullettmp = EasyObjectPool.instance.GetObjectFromPool(listWeapon[indexgun], firepos.position, _firepos.rotation);
            bullet.Add(bullettmp);
            Rigidbody2D rbtmp = bullettmp.GetComponent<Rigidbody2D>();
            rb.Add(rbtmp);
            Vector3 destination = playerpos.position;
            float x = UnityEngine.Random.Range(-0.1f, 0.1f);
            float y = UnityEngine.Random.Range(0.2f, 0.25f);
            destination = destination + new Vector3(x, y, 0);
            StartCoroutine(Move(sprite[i], destination));
            Quaternion angle = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-deflection[indexgun], deflection[indexgun]));
            Vector2 shootDirect = angle * direct;
            rb[i].AddForce(shootDirect * listForce[indexgun], ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(flyTime[indexgun]);
        bulletparent.SetActive(false);
        for (int i = 0; i < numberofBullets[indexgun]; i++)
        {
            if(disappear)
            {
                rb[i].velocity = Vector2.zero;
                BulletShotgun shot = bullet[i].GetComponent<BulletShotgun>();
                StartCoroutine(shot.Disappear());
            }
            else
            {
                bullet[i].SetActive(false);
                EasyObjectPool.instance.ReturnObjectToPool(bullet[i]);
            }
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
