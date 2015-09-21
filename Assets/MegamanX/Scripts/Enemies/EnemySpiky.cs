using UnityEngine;
using System.Collections;

public class EnemySpiky : Enemy
{
	private Vector2 moveVector = Vector2.zero;
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
		TranslateMove (moveLeft, speed, Time.fixedDeltaTime, moveVector, myAnimator, "hSpeed");
	}

	void OnCollisionEnter2D(Collision2D localCollision2D)
	{
		Damage (localCollision2D.gameObject, damage);
	}
}
