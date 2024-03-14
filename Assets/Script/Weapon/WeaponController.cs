using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject chooseWeapon;
    [SerializeField] SpriteRenderer weaponSprite;
    [SerializeField] SpriteRenderer gun;
    [SerializeField] List<Sprite> spritesWeapon = new List<Sprite>();
    [SerializeField] List<Sprite> spritesGun = new List<Sprite>();
    int index = 0;
    bool isChoosing = false;
    float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q) && !isChoosing)
        {
            time = -0.2f;
            isChoosing = true;
            weaponSprite.sprite = spritesWeapon[index];
            chooseWeapon.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isChoosing)
        {
            if (index < spritesWeapon.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            weaponSprite.sprite = spritesWeapon[index];
        }
        if (Input.GetKeyDown(KeyCode.Q) && isChoosing && time > 0)
        {
            isChoosing = false;
            gun.sprite = spritesGun[index];
            BulletController.Instance.indexgun = index;
            chooseWeapon.SetActive(false);
        }
    }
}
