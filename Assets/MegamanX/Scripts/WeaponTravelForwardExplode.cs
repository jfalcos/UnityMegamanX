using UnityEngine;
using System.Collections;

public class WeaponTravelForwardExplode : DamageSource
{
	private Rigidbody2D rigidbody2D = null;
	public float damage = 1f;
	public float speed = 2f;
	public float duration = 1f;

	void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	void Start()
	{
		Invoke ("DoTimedDestroy", duration);
	}

	void Update()
	{
		if(transform.localScale.x < 0)
		{
			rigidbody2D.velocity = new Vector2 (transform.right.x * -1 * speed, 0f);
		}
		else
		{
			rigidbody2D.velocity = new Vector2 (transform.right.x * speed, 0f);
		}
	}

	void OnCollisionEnter2D(Collision2D localCollision2D)
	{
		SelfDestructAndDoDamage (localCollision2D.gameObject);
	}

	void OnTriggerEnter2D(Collider2D localCollider2D)
	{
		SelfDestructAndDoDamage (localCollider2D.gameObject);
	}

	void SelfDestructAndDoDamage(GameObject collidingObject)
	{
		if(CanCollideWith(collidingObject))
		{
			Hitpoints collidingObjectHitpoints = collidingObject.GetComponent<Hitpoints> ();
			if(collidingObjectHitpoints != null)
			{
				collidingObjectHitpoints.Damage (damage, gameObject, damageSourceOwner);
			}
			Destroy (gameObject);
		}
	}

	void DoTimedDestroy()
	{
		Destroy (gameObject);
	}
}
