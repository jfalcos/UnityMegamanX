using UnityEngine;
using System.Collections;

public class WallJump2D : MonoBehaviour
{
	private bool lockWallJump = false;
	public float lockTime = 0.2f;
	public Vector2 jumpForce = Vector2.zero;
	public string jumpAnimationName = "";
	public string jumpAnimationTriggerName = "";

	public void WallJump(Animator myAnimator, Rigidbody2D myRigidbody2D, WallCheck[] allWallChecks)
	{
		if(!lockWallJump)
		{
			foreach(WallCheck localWallCheck in allWallChecks)
			{
				if(localWallCheck.walled)
				{
					lockWallJump = true;
					if(myRigidbody2D.transform.localScale.x < 0)
					{
						myRigidbody2D.AddForce(new Vector2(myRigidbody2D.velocity.x + jumpForce.x, jumpForce.y));
					}
					else
					{
						myRigidbody2D.AddForce(new Vector2(myRigidbody2D.velocity.x - jumpForce.x, jumpForce.y));
					}

					if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName(jumpAnimationName))
					{
						myAnimator.SetTrigger(jumpAnimationTriggerName);
					}
					Invoke ("UnlockWallJump", lockTime);
					break;
				}
			}
		}
	}

	private void UnlockWallJump()
	{
		lockWallJump = false;
	}
}