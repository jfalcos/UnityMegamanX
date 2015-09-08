using UnityEngine;
using System.Collections;

public class EnemyIntroStageVile : MonoBehaviour
{
	private MegamanController megamanController = null;
	private Rigidbody2D myRigidbody2D = null;
	public bool jumping = false;
	public bool moving = false;
	private Collider2D myCollider2D = null;
	public Animator myAnimator = null;
	public GroundCheck groundCheck = null;
	public Collider2D deathRogumersLiftCollder = null;
	public float walkSpeed = 1f;
	public float slideSpeed = 2f;
	public float jumpForce = 50f;
	public float attackDistance = 1f;
	public float walkDistance = 2f;
	public float slideDistance = 4f;
	public bool readyToAttack = false;

	void Awake()
	{
		megamanController = GameObject.FindObjectOfType<MegamanController> ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		myCollider2D = GetComponent<Collider2D> ();
	}

	void Start()
	{
		StartCoroutine (LiftJump ());
	}

	void Update()
	{
		if(readyToAttack)
		{
			float distance = Vector3.Distance (transform.position, megamanController.transform.position);

			if(distance >= walkDistance && distance < slideDistance && !moving)
			{
				moving = true;
				StartCoroutine(WalkTowardsTarget(megamanController.transform.position));
			}
			else if(distance >= slideDistance && !moving)
			{
				moving = true;
				StartCoroutine(SlideTowardsTarget(megamanController.transform.position));
			}

			if(distance <= attackDistance)
			{
				if(!IsInvoking("Attack"))
				{
					Invoke("Attack", 1f);
				}
			}
		}
	}

	void FixedUpdate()
	{
		if(jumping && groundCheck.grounded)
		{
			Land();
		}
	}

	IEnumerator LiftJump()
	{
		yield return new WaitForSeconds (1f);
		transform.SetParent (null);
		Physics2D.IgnoreCollision (myCollider2D, deathRogumersLiftCollder);
		Jump ();
	}

	public void Jump()
	{
		if(groundCheck.grounded)
		{
			myRigidbody2D.AddForce(new Vector2(0, jumpForce));
			jumping = true;
			SetIdleAnimation (false);
			SetWalkAnimation (false);
			myAnimator.SetTrigger ("jump");
			Invoke ("Laugh", 1f);
		}
	}

	public void JumpBackwards()
	{
	}

	public void Land()
	{
		jumping = false;
		SetIdleAnimation (false);
		SetWalkAnimation (false);
		myAnimator.SetTrigger ("land");
	}

	IEnumerator WalkTowardsTarget(Vector3 targetPosition)
	{
		print ("WalkTowardsTarget");
		SetIdleAnimation (false);
		SetWalkAnimation (true);

		float distance = Vector3.Distance (transform.position, targetPosition);
		Vector3 translationVector = Vector3.zero;
		while(distance > walkDistance)
		{
			Debug.Log("Walking Towards Target - Distance = " + distance);
			distance = Vector3.Distance (transform.position, targetPosition) % 1;
			translationVector = targetPosition - transform.position;
			translationVector.z = 0f;
			transform.Translate(translationVector * walkSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.03f);
		}
		moving = false;
		yield return null;
	}
	
	IEnumerator SlideTowardsTarget(Vector3 targetPosition)
	{
		print ("SlideTowardsTarget");
		SetIdleAnimation (false);
		SetWalkAnimation (false);
		
		float distance = Vector3.Distance (transform.position, targetPosition);
		Vector3 translationVector = Vector3.zero;
		while(distance > slideDistance)
		{
			Debug.Log("Sliding Towards Target - Distance = " + distance);
			distance = Vector3.Distance (transform.position, targetPosition) % 1;
			translationVector = targetPosition - transform.position;
			translationVector.z = 0f;
			transform.Translate(translationVector * slideSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.03f);
		}
		moving = false;
		yield return null;
	}

	public void Attack()
	{
		SetIdleAnimation (false);
		SetWalkAnimation (false);
		myAnimator.SetTrigger ("attack");
	}

	public void Laugh()
	{
		SetIdleAnimation (false);
		SetWalkAnimation (false);
		myAnimator.SetTrigger ("laugh");
	}

	void SetIdleAnimation(bool enabled)
	{
		myAnimator.SetBool ("idle", enabled);
	}

	void SetWalkAnimation(bool enabled)
	{
		myAnimator.SetBool ("walk", enabled);
	}

	void OnDrawGizmos()
	{
		Vector3 walkVectorStart = new Vector3 (transform.position.x, transform.position.y + 0.4f, transform.position.z);
		Vector3 slideVectorStart = new Vector3 (transform.position.x, transform.position.y + 0.3f, transform.position.z);
		Vector3 attackVectorStart = new Vector3 (transform.position.x, transform.position.y + 0.2f, transform.position.z);
		
		Vector3 walkVectorEnd = new Vector3 (transform.position.x + transform.right.x * -1 * walkDistance, transform.position.y + 0.4f, transform.position.z);
		Vector3 slideVectorEnd = new Vector3 (transform.position.x + transform.right.x * -1 * slideDistance, transform.position.y + 0.3f, transform.position.z);
		Vector3 attackVectorEnd = new Vector3 (transform.position.x + transform.right.x * -1 * attackDistance, transform.position.y + 0.2f, transform.position.z);

		Gizmos.DrawLine (walkVectorStart, walkVectorEnd);
		Gizmos.DrawLine (slideVectorStart, slideVectorEnd);
		Gizmos.DrawLine (attackVectorStart, attackVectorEnd);
	}
}
