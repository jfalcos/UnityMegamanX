using UnityEngine;
using System.Collections;

public class EnemyIntroStageVile : Enemy
{
	private MegamanController megamanController = null;
	private Rigidbody2D myRigidbody2D = null;
	private bool jumping = false;
	private bool moving = false;
	private bool damageLock = false;
	private bool walkToMegaman = false;
	private GameObject energyBallInstance = null;
	public Collider2D myCollider2D = null;
	public Collider2D myTriggerCollider2D = null;
	public GameObject vilesEnergyBall = null;
	public Transform energyBallSpawnMarker = null;
	public Transform handGrabPositionMarker = null;
	public GroundCheck groundCheck = null;
	public Collider2D deathRogumersLiftCollider = null;
	public float damage = 1f;
	public float constantDamageDelay = 0.5f;
	public float walkSpeed = 1f;
	public float slideSpeed = 2f;
	public Vector2 jumpForce = new Vector2 (25f, 50f);
	public float attackDistance = 1f;
	public float walkDistance = 2f;
	public float slideDistance = 4f;
	public bool readyToAttack = false;

	protected override void Awake()
	{
		base.Awake ();
		megamanController = GameObject.FindObjectOfType<MegamanController> ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	protected override void Start()
	{
		base.Start ();
		Physics2D.IgnoreCollision (myCollider2D, megamanController.myCollider2D);
		StartCoroutine (LiftJump ());
	}

	void Update()
	{
		RotateToMegaman ();

		if(readyToAttack)
		{
			float distance = Vector3.Distance (transform.position, megamanController.transform.position);

			if(distance >= walkDistance && distance < slideDistance && !moving)
			{
				moving = true;
				WalkTowardsTarget(megamanController.transform.position);
			}
			else if(distance >= slideDistance && !moving)
			{
				moving = true;
				SlideTowardsTarget(megamanController.transform.position);
			}

			if(distance <= attackDistance)
			{
				if(!IsInvoking("Attack"))
				{
					Invoke("Attack", 1f);
				}
			}

			if(!groundCheck.grounded && megamanController.healthInPercentage < 25)
			{
				ShootEnergyBall();
			}
		}

		if(walkToMegaman)
		{
			WalkTowardsTarget(megamanController.transform.position + Vector3.right * 0.5f);
			if(ProximityCheck(megamanController.transform.position + Vector3.right * 0.5f, 0.25f) && ((megamanController.transform.position.x - transform.position.x) < 0))
			{
				walkToMegaman = false;
				myAnimator.SetTrigger ("grabMegaman");
				megamanController.OnGrabbedByEnemy (handGrabPositionMarker.position);
				IntroStageZerosCutscene zerosCutscene = GameObject.FindObjectOfType<IntroStageZerosCutscene>();
				StartCoroutine(zerosCutscene.BeginCutscene());
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

	void OnTriggerEnter2D(Collider2D localCollider2D)
	{
		if(!damageLock)
		{
			if(CanCollideWith(localCollider2D.gameObject))
			{
				Hitpoints hitpoints = megamanController.gameObject.GetComponent<Hitpoints>();
				hitpoints.Damage(damage, gameObject, gameObject);
				damageLock = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D localCollider2D)
	{
		OnTriggerEnter2D (localCollider2D);
	}

	IEnumerator LiftJump()
	{
		yield return new WaitForSeconds (1f);
		transform.SetParent (null);
		SetIgnoreLiftCollision (true);
		IntroJump ();
	}

	public void IntroJump()
	{
		if(groundCheck.grounded)
		{
			myRigidbody2D.AddForce(new Vector2(0, jumpForce.y));
			jumping = true;
			SetIdleAnimation (false);
			SetWalkAnimation (false);
			myAnimator.SetTrigger ("jump");
			Invoke ("Laugh", 1f);
		}
	}

	public void JumpUp()
	{
		if(groundCheck.grounded)
		{
			myRigidbody2D.AddForce(new Vector2(0, jumpForce.y));
			jumping = true;
			SetIdleAnimation (false);
			SetWalkAnimation (false);
			myAnimator.SetTrigger ("jump");
		}
	}

	public void JumpForward(Vector3 toTarget)
	{
		if(groundCheck.grounded)
		{
			Vector3 directionVector = toTarget - transform.position; //My front
			directionVector.x = Mathf.Clamp(directionVector.x, -1, 1);
			myRigidbody2D.AddForce(new Vector2(directionVector.x * jumpForce.x, jumpForce.y));
			jumping = true;
			SetIdleAnimation (false);
			SetWalkAnimation (false);
			myAnimator.SetTrigger ("jump");
		}
	}

	public void JumpBackwards(Vector3 fromTarget)
	{
		if(groundCheck.grounded)
		{
			Vector3 directionVector = fromTarget + transform.position; //My back
			directionVector.x = Mathf.Clamp(directionVector.x, -1, 1);
			myRigidbody2D.AddForce(new Vector2(directionVector.x * jumpForce.x, jumpForce.y));
			jumping = true;
			SetIdleAnimation (false);
			SetWalkAnimation (false);
			myAnimator.SetTrigger ("jump");
		}
	}

	void ShootEnergyBall()
	{
		if(energyBallInstance == null)
		{
			energyBallInstance = Instantiate (vilesEnergyBall, energyBallSpawnMarker.position, Quaternion.identity) as GameObject;
		}
	}

	public void Land()
	{
		jumping = false;
		SetIdleAnimation (false);
		SetWalkAnimation (false);
		myAnimator.SetTrigger ("land");
	}

	void WalkTowardsTarget(Vector3 targetPosition)
	{
		SetIdleAnimation (false);
		SetWalkAnimation (true);

		Vector3 translationVector = Vector3.zero;
		translationVector = targetPosition - transform.position;
		translationVector.z = 0f;
		transform.Translate(translationVector * walkSpeed * Time.deltaTime);
		moving = false;
	}
	
	void SlideTowardsTarget(Vector3 targetPosition)
	{
		SetIdleAnimation (false);
		SetWalkAnimation (false);

		Vector3 translationVector = Vector3.zero;
		translationVector = targetPosition - transform.position;
		translationVector.z = 0f;
		transform.Translate(translationVector * slideSpeed * Time.deltaTime);
		moving = false;
	}

	public void BreakArm()
	{
		myRigidbody2D.AddForce (new Vector2 (4000f, 2000f));
		myAnimator.SetTrigger ("breakArm");
	}

	public void SetIgnoreLiftCollision(bool ignoreCollision)
	{
		Physics2D.IgnoreCollision (myCollider2D, deathRogumersLiftCollider, ignoreCollision);
	}

	public void Attack()
	{
		int attackChoice = Random.Range (0, 100);

		if(megamanController.healthInPercentage < 25)
		{
			//Same moves as everything below the else{} statement except for the PuncAttack().
			if(attackChoice < 33)
			{
				JumpForward(megamanController.transform.position);
			}
			else if(attackChoice >= 33 && attackChoice < 66)
			{
				JumpUp();
			}
			else if(attackChoice >= 66 && attackChoice < 100)
			{
				JumpBackwards(megamanController.transform.position);
			}
		}
		else
		{
			if(attackChoice < 25)
			{
				JumpForward(megamanController.transform.position);
			}
			else if(attackChoice >= 25 && attackChoice < 50)
			{
				JumpUp();
			}
			else if(attackChoice >= 50 && attackChoice < 75)
			{
				JumpBackwards(megamanController.transform.position);
			}
			else if(attackChoice >= 75 && attackChoice <= 100)
			{
				PunchAttack();
			}

			damageLock = !damageLock;
		}
	}

	void PunchAttack()
	{
		myAnimator.SetTrigger ("attack");
	}

	IEnumerator WalkAttack()
	{
		Vector3 targetPosition = megamanController.transform.position + (megamanController.transform.position - transform.position) / 2f;
		SetIdleAnimation (false);
		SetWalkAnimation (true);

		while(!ProximityCheck(targetPosition, 0.6f))
		{
			print("Proximity Check: " + (Mathf.Abs (Vector3.Distance (transform.position, targetPosition)) <= 0.6f));
			print("Distance: " + Mathf.Abs (Vector3.Distance (transform.position, targetPosition)));
			Vector3 translationVector = Vector3.zero;
			translationVector = targetPosition - transform.position;
			translationVector.z = 0f;
			transform.Translate(translationVector * walkSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.03f);
		}
		moving = false;
		yield return null;
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

	private void RotateToMegaman()
	{
		Vector3 myScale = transform.localScale;
		Vector3 directionVector = megamanController.transform.position - transform.position;

		if(directionVector.x < 0 )
		{
			if(myScale.x < 0)
			{
				myScale.x *= -1;
			}
		}
		else if(directionVector.x > 0)
		{
			if(myScale.x > 0)
			{
				if(myScale.x > 0)
				{
					myScale.x *= -1;
				}
			}
		}

		transform.localScale = myScale;
	}

	void DamageUnlock()
	{
		damageLock = false;
	}

	public void OnCaptureMegaman()
	{
		readyToAttack = false;
		CancelInvoke ();
		walkToMegaman = true;
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
