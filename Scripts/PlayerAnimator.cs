using System;
using UnityEngine;


public enum AnimationPlayer
{
    Run,
    Idle,
    BoxWalk,
    BoxStay,
    RocketRun,
    RocketStay
}


public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private AnimationPlayer _oldAnimation;

    private void Start()
    {
        _oldAnimation = AnimationPlayer.Idle;
        _animator.SetBool(_oldAnimation.ToString(), true);
    }

    private void OnEnable() => 
        EventSystem.OnChangeAnimation += ChangeAnimation;
    private void OnDisable() => 
        EventSystem.OnChangeAnimation -= ChangeAnimation;


    private void ChangeAnimation(AnimationPlayer animation) 
    {
        if (animation != _oldAnimation)
        {
            _animator.SetBool(animation.ToString(), true);
            _oldAnimation = animation;
            foreach (string anim in Enum.GetNames(typeof(AnimationPlayer)))
            {
                if (animation.ToString() != anim)
                {
                    _animator.SetBool(anim, false);
                }
            }
        }
        

    }


}
