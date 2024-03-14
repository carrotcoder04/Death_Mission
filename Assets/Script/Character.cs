using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnim;
    protected void OnInit()
    {
        ChangeAnim("Idle");
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
