using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Hitpoints))]
public class Enemy : DamageSource
{
	protected Hitpoints _hitpoints = null;
	public GameObject onDestroyFX = null;
	public Animator myAnimator = null;

	protected virtual void Awake()
	{
		_hitpoints = GetComponent<Hitpoints> ();
	}

	protected virtual void Start()
	{
	}

	protected virtual void OnEnable()
	{
		_hitpoints.onDamage += OnDamage;
		_hitpoints.onKill += OnKill;
	}

	protected virtual void OnDisable()
	{
		_hitpoints.onDamage -= OnDamage;
		_hitpoints.onKill -= OnKill;
	}

	/*
		Don't waste memory. Because not every enemy will move, the subclass will have to provide the variables to move.
		But because many enemies will move we provide a base method subclasses can call.
		
	 */

	/*
		Moves without using velocity and acceleration.
	 */
	protected void TranslateMove(bool moveLeft, float speed, float smooth, Vector2 moveVector)
	{
		if(moveLeft)
		{
			moveVector.x = transform.right.x * -1 * speed;
			moveVector.y = 0f;
		}
		else
		{
			moveVector.x = transform.right.x * speed;
			moveVector.y = 0f;
		}
		transform.Translate (moveVector * speed * smooth);
	}
	
	protected void TranslateMove(bool moveLeft, float speed, float smooth, Vector2 moveVector, Animator myAnimator, string myAnimatorParameter)
	{
		TranslateMove (moveLeft, speed, smooth, moveVector);
		myAnimator.SetFloat (myAnimatorParameter, speed);
	}

	/*
		Moves using velocity and acceleration.
	 */
	protected void VelocityMove(bool moveLeft, float speed, float smooth, Vector2 moveVector, Rigidbody2D myRigidbody2D)
	{
		if(moveLeft)
		{
			moveVector.x = transform.right.x * -1 * speed;
			moveVector.y = 0f;
		}
		else
		{
			moveVector.x = transform.right.x * speed;
			moveVector.y = 0f;
		}
		myRigidbody2D.velocity = moveVector;
	}
	
	protected void VelocityMove(bool moveLeft, float speed, float smooth, Vector2 moveVector, Rigidbody2D myRigidbody2D, Animator myAnimator, string myAnimatorParameter)
	{
		VelocityMove (moveLeft, speed, smooth, moveVector, myRigidbody2D);
		myAnimator.SetFloat (myAnimatorParameter, speed);
	}

	protected virtual void OnDamage(Hitpoints hitpoints)
	{
		if(myAnimator != null)
		{
			myAnimator.SetTrigger("damage");
		}
	}

	protected virtual void OnKill(Hitpoints hitpoints)
	{
		DestroyMe ();
	}

	protected virtual void PlayDamageAnimation()
	{

	}

	protected virtual void DestroyMe()
	{
		if(onDestroyFX != null)
		{
			GameObject localGameObject = Instantiate(onDestroyFX, onDestroyFX.transform.position, onDestroyFX.transform.rotation) as GameObject;
			localGameObject.SetActive(true);
		}
		Destroy (gameObject);
	}

	protected bool ProximityCheck(MegamanController megamanController, float distance)
	{
		return (Mathf.Abs (Vector3.Distance (transform.position, megamanController.transform.position)) <= distance);
	}

	protected bool ProximityCheck(Vector3 goalPosition, float distance)
	{
		return (Mathf.Abs (Vector3.Distance (transform.position, goalPosition)) <= distance);
	}

	protected bool ProximityCheckAgainstMegamanController(float radius, LayerMask layerMask)
	{
		bool success = false;
		Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask.value);

		foreach(Collider2D localCollider2D in closeColliders)
		{
			MegamanController megamanController = localCollider2D.gameObject.GetComponent<MegamanController>();
			if(megamanController != null)
			{
				success = true;
			}
		}

		return success;
	}

	protected void SafeStopCoroutine(Coroutine routine)
	{
		if(routine != null)
		{
			StopCoroutine(routine);
		}
	}
	
	public Hitpoints hitpoints
	{
		get
		{
			return _hitpoints;
		}
	}
}
