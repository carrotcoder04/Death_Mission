using DG.Tweening;
using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletController : Singleton<BulletController>
{
    public bool isDead = false;
    public int indexgun = 0;
    public float maxslashTime = 0.1f;
    private float firetime = 0;
    private float slashtime = 0;
    private readonly List<string> listWeapon = new List<string>() { Constants.BULLETREVOLVER, Constants.BULLETREVOLVER, Constants.BULLETSHOTGUN, Constants.GRENADE, Constants.ROCKET};
    private readonly List<string> listSprite = new List<string>() { Constants.BULLETREVOLVERSPRTE, Constants.BULLETREVOLVERSPRTE, Constants.BULLETSHOTGUNSPRITE};
    private readonly List<int> numberofBullets = new List<int>() { 1, 1, 5, 1, 1 };
    private readonly List<int> deflection = new List<int>() { 0, 2, 15, 0, 0 };
    private readonly List<float> maxfireTime = new List<float> { 0.25f , 0.2f , 0.75f , 1f , 1f};
    private readonly List<float> listForce = new List<float> { 6f,5f, 5f, 3f, 3f };
    private readonly List<float> flyTime = new List<float> { 0.5f, 0.6f, 0.18f, 1.2f, 1.2f };
    [SerializeField] Transform firepos;
    [SerializeField] Transform _firepos;
    [SerializeField] Transform playerpos;
    [SerializeField] GameObject bulletparent;
    [SerializeField] GameObject slash;
    private void Update()
    {
        firetime += Time.deltaTime;
        slashtime += Time.deltaTime;
        if (isDead) return;
        if (Input.GetMouseButton(0) && firetime > maxfireTime[indexgun])
        {
            if (UIManager.Instance.ammo[indexgun] > 0)
            {
                Actions.Fire?.Invoke();
                StartCoroutine(Fire());
                firetime = 0;
            }
        }
        if (Input.GetMouseButtonDown(1) && slashtime > maxslashTime)
        {
            slashtime = 0;
            StartCoroutine(Slash());
            CameraFollower.Instance.CameraShake(0.02f);
        }
    }
    IEnumerator Fire()
    {
        if (indexgun == 2)
        {
            CameraFollower.Instance.CameraShake(0.1f);
        }
        else if (indexgun == 0 || indexgun == 1)
        {
            CameraFollower.Instance.CameraShake(0.01f);
        }
        else if (indexgun == 3 || indexgun == 4)
        {
            CameraFollower.Instance.CameraShake(0.15f);
            yield return new WaitForSeconds(0.1f);
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
            if(indexgun != 3 && indexgun != 4)
            {
                GameObject bulletsprite = EasyObjectPool.instance.GetObjectFromPool(listSprite[indexgun], playerpos.position, _firepos.rotation);
                sprite.Add(bulletsprite);
                Vector3 destination = playerpos.position;
                float x = UnityEngine.Random.Range(-0.1f, 0.1f);
                float y = UnityEngine.Random.Range(0.2f, 0.25f);
                destination = destination + new Vector3(x, y, 0);
                StartCoroutine(Move(sprite[i], destination));
            }
            GameObject bullettmp = EasyObjectPool.instance.GetObjectFromPool(listWeapon[indexgun], firepos.position, _firepos.rotation);
            bullet.Add(bullettmp);
            Rigidbody2D rbtmp = bullettmp.GetComponent<Rigidbody2D>();
            rb.Add(rbtmp);
            Quaternion angle = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-deflection[indexgun], deflection[indexgun]));
            Vector3 shootDirect = angle * direct;
            rb[i].AddForce(shootDirect * listForce[indexgun],ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.03f);
        }
        if (indexgun == 2) yield return new WaitForSeconds(flyTime[indexgun]);
        else if (indexgun == 0 || indexgun == 1)
        {
            float time = 0;
            BulletRevolverHit bulletRevolverHit = bullet[0].GetComponent<BulletRevolverHit>();
            while(time < flyTime[indexgun])
            {
                time += Time.deltaTime;
                if (bulletRevolverHit.isHit)
                {
                    bulletparent.SetActive(false);
                    yield break;
                }
                yield return null;
            }
        }
        else if(indexgun == 3)
        {
            float time = 0;
            Grenade grenade = bullet[0].GetComponent<Grenade>();
            while (time < flyTime[indexgun])
            {
                time += Time.deltaTime;
                if (grenade.isHit)
                {
                    bulletparent.SetActive(false);
                    yield break;
                }
                yield return null;
            }
        }
        else if (indexgun == 4)
        {
            float time = 0;
            Rocket rocket = bullet[0].GetComponent<Rocket>();
            while (time < flyTime[indexgun])
            {
                time += Time.deltaTime;
                if (rocket.isHit)
                {
                    bulletparent.SetActive(false);
                    yield break;
                }
                yield return null;
            }
        }
        bulletparent.SetActive(false);
        for (int i = 0; i < numberofBullets[indexgun]; i++)
        {
            if (indexgun == 2)
            {
                if (bullet[i].activeInHierarchy)
                {
                    rb[i].velocity = Vector2.zero;
                    BulletShotgun shot = bullet[i].GetComponent<BulletShotgun>();
                    StartCoroutine(shot.Disappear());
                }
            }
            else if (indexgun == 0 || indexgun == 1)
            {
                if (bullet[i].activeInHierarchy)
                {
                    bullet[i].SetActive(false);
                }
            }
        }
        yield break;
    }
    IEnumerator Slash()
    {
        slash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        slash.SetActive(false);
        yield break;
    }
    IEnumerator Move(GameObject bullet,Vector3 destination)
    {
        bullet.transform.DOMove(destination,0.1f);
        yield break;
    }
}
