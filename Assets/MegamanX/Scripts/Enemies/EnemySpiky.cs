using UnityEngine;
using System.Collections;

public class EnemySpiky : Enemy
{
	private Vector2 moveVector = Vector2.zero;
	public Animator myAnimator = null;
	public float damage = 1f;
	public float speed = 1f;
	public bool moveLeft = true;

	protected override void Awake()
	{
		base.Awake ();
	}

	protected override void Start()
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
//		myRigidbody2D.velocity = moveVector;
		transform.Translate (moveVector * speed * Time.deltaTime);
		myAnimator.SetFloat ("hSpeed", speed);
	}

	void OnCollisionEnter2D(Collision2D localCollision2D)
	{
		if(CanCollideWith(localCollision2D.gameObject))
		{
			Hitpoints hitpoints = localCollision2D.gameObject.GetComponent<Hitpoints>();
			if(hitpoints != null)
			{
				hitpoints.Damage(damage, gameObject, gameObject);
			}
		}
	}
}
