using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    // Update is called once per frame
    void Update()
    {
        animator = FindObjectOfType<Animator>();
    }

    public void AnimJump()
    {
        animator.SetTrigger("Jump");
    }

    public void AnimSetRightLeftOff()
    {
        animator.SetBool("LeftStrafe", false);
        animator.SetBool("RightStrafe", false);
    }

    public void AnimLeftStrafe()
    {
        animator.SetBool("RightStrafe", false);
        animator.SetBool("LeftStrafe", true);
    }

    public void AnimRightStrafe()
    {
        animator.SetBool("LeftStrafe", false);
        animator.SetBool("RightStrafe", true);
    }

    public void AnimRun()
    {
        animator.SetInteger("State", 1);
    }

    public void AnimRespawn()
    {
        animator.SetInteger("State", 2);
    }

    public void AnimRestart()
    {
        animator.SetInteger("State", 3);
    }

	public void AnimReset()
	{
		animator.SetInteger("State", 4);
	}
}
