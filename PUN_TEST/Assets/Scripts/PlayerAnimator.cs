using System;
using Photon.Pun;
using UnityEngine;

public class PlayerAnimator : MonoBehaviourPun
{
    public Animator anim;
    public AnimationState lastAnim = AnimationState.Idle;
    private PlayerInput _input;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            AnimationSelector();
        }
    }

    private void AnimationSelector()
    {
        return;
        AnimationState animationState = _input.isMove ? AnimationState.Run : AnimationState.Idle;

        if (lastAnim != animationState)
        {
            lastAnim = animationState;
            string trigger = lastAnim.ToString();
            anim.SetTrigger(trigger);
        }
    }
}