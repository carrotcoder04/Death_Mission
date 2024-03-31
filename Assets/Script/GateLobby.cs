
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLobby : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameController.Instance.LoadSceen(1);
            UIManager.Instance.StartCounter();
        }
    }
}
