using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public string lastAnim = "Idle";

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void SetAnimation(string nameAnim)
    {
        if (nameAnim != lastAnim)
        {
            anim.SetTrigger(nameAnim);
            lastAnim = nameAnim;
        }
    }
}
