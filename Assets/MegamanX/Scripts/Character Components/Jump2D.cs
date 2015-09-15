using UnityEngine;
using System.Collections;

public class Jump2D : MonoBehaviour
{
	private bool lockJump = false;
	public float lockTime = 0.1f;
	public Vector2 jumpForce = Vector2.zero;
	public string jumpAnimationName = "";
	public string jumpAnimationTriggerName = "";

	public void Jump(Animator myAnimator, Rigidbody2D myRigidbody2D, GroundCheck groundCheck)
	{
		if(groundCheck.grounded && !lockJump)
		{
			lockJump = true;
			if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName(jumpAnimationName))
			{
				myAnimator.SetTrigger(jumpAnimationTriggerName);
				myRigidbody2D.AddForce(jumpForce);
			}
			Invoke("UnlockJump", lockTime);
		}
	}

	private void UnlockJump()
	{
		lockJump = false;
	}
}