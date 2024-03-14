using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotgun : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnim;
    private void OnDisable()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    private void OnEnable()
    {
        ChangeAnim("Init");
    }
    public IEnumerator Disappear()
    {
        ChangeAnim("Disappear");
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (currentAnim != null) animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
