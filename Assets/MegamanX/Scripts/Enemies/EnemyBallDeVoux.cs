using UnityEngine;
using System.Collections;

public class EnemyBallDeVoux : Enemy
{
	private Rigidbody2D myRigidbody2D = null;
	private Vector2 moveVector = Vector2.zero;
	public float damage = 1f;
	public float speed = 1f;
	public bool moveLeft = true;
	
	protected override void Awake()
	{
		base.Awake ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	void FixedUpdate()
	{
		VelocityMove (moveLeft, speed, Time.fixedDeltaTime, moveVector, myRigidbody2D, myAnimator, "hSpeed");
	}
	
	void OnCollisionEnter2D(Collision2D localCollision2D)
	{
		Damage (localCollision2D.gameObject, damage);
	}
}
