using UnityEngine;
using System.Collections;

public class Enemy2 : DamageSource
{
	private Rigidbody2D myRigidbody2D = null;
	private Animator myAnimator = null;
	private Vector2 moveVector = Vector2.zero;
	public float damage = 1f;
	public float speed = 1f;
	public bool moveLeft = true;

	void Awake()
	{
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}

	void Start()
	{
	}

	void FixedUpdate()
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

	void OnCollisionEnter2D(Collision2D localCollision2D)
	{
		if(CanCollideWith(localCollision2D.gameObject))
		{
			Hitpoints hitpoints = localCollision2D.gameObject.GetComponent<Hitpoints>();
			hitpoints.Damage(damage, gameObject, gameObject);
		}
	}
}
