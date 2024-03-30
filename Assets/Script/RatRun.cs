using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RatRun : MonoBehaviour
{
    [SerializeField] Transform target;
    void Start()
    {
        transform.DOMove(target.position+new Vector3(0.5f,0,0),15f).SetEase(Ease.Linear).SetLoops(-1);
    }
}
