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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isChoosing)
        {
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
        if (Input.GetKeyDown(KeyCode.C) && isChoosing)
        {
            isChoosing = false;
            gun.sprite = spritesGun[index];
            BulletController.Instance.indexgun = index;
            if(index == 0)
            {
                BulletController.Instance.maxfireTime = 0.3f;
            } 
            else if(index == 1)
            {
                BulletController.Instance.maxfireTime = 0.2f;
            }
            chooseWeapon.SetActive(false);
        }
    }
}
