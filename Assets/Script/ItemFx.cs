using UnityEngine;
using DG.Tweening;
public class ItemFx : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveY(transform.position.y + 0.05f,0.5f).SetLoops(-1);
    }
}
