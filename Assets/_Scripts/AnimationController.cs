using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Start() {
        animator.SetBool("isRunning", false);
    }

    public void StartWalkingAnim() {
        animator.SetBool("isRunning",true);
    }

    public void StopWalkingAnim() {
        animator.SetBool("isRunning", false);
    }
}
