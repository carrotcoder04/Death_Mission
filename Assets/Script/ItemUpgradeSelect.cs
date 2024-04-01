using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUpgradeSelect : MonoBehaviour, IPointerEnterHandler
{
    public byte id;
    public GameObject item;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter" + eventData.clickCount);
    }
}
