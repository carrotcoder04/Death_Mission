using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMap1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(Map1Controller.Instance.isWin)
            {
                Actions.GoIntotTheGate?.Invoke();
            }
        }
    }
}
