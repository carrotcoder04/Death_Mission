using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject door;
    string opening = "Opening";
    string closing = "Closing";
    private bool isOpen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isOpen = true;
            StartCoroutine(Opening());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isOpen = false;
            StartCoroutine(Closing());
        }
    }
    IEnumerator Opening()
    {
        animator.SetTrigger(opening);
        yield return new WaitForSeconds(1f);
        door.SetActive(false);
        yield break;
    }
    IEnumerator Closing()
    {
        yield return new WaitForSeconds(4f);
        if(isOpen)
        {
            yield return StartCoroutine(Closing());
            yield break;
        }
        else
        {
            door.SetActive(true);
            animator.SetTrigger(closing);
            StopAllCoroutines();
        }
    }
}
