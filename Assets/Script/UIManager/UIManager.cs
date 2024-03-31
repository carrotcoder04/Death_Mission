using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Image weaponRenderer;
    [SerializeField] TextMeshProUGUI ammoTxt;
    [SerializeField] TextMeshProUGUI goldTxt;
    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] List<Sprite> weaponSprite = new List<Sprite>();
    public List<int> ammo = new List<int>();
    int indexgun = 0;
    void Start()
    {
        Actions.Fire += Fire;
        Actions.ChangeWeapon += ChangeWeapon;
        Actions.OnChangeGold += OnChangeGold;
        ammo.Add(300);
        ammo.Add(200);
        ammo.Add(188);
        ammo.Add(200);
        ammo.Add(123);
        ammoTxt.text = ammo[indexgun].ToString();
    }

    private void OnChangeGold(int gold)
    {
        GameController2.Instance.totalgold += gold;
        goldTxt.text = GameController2.Instance.totalgold.ToString();
    }
    void ChangeWeapon(int index)
    {
        indexgun = index;
        weaponRenderer.sprite = weaponSprite[index];
        ammoTxt.text = ammo[index].ToString();
    }
    void Fire()
    {
        ammo[indexgun]--;
        ammoTxt.text = ammo[indexgun].ToString();
    }
    public void IncreaseAmmmo(int ammo,int index)
    {
        this.ammo[index] += ammo;
        if(indexgun == index) ammoTxt.text = this.ammo[index].ToString();
    }
    public void StartCounter()
    {
        StartCoroutine(CounterTime());
    }
    IEnumerator CounterTime()
    {
        int time = -2;
        while(true)
        {
            time += 1;
            int min = time / 60;
            int sec = time % 60;
            string _min = min < 10 ? "0" + min.ToString() : min.ToString();
            string _sec = sec < 10 ? "0" + sec.ToString() : sec.ToString();
            timeTxt.text = _min + " : " + _sec;
            yield return new WaitForSeconds(1f);
        }
    }
}
