using UnityEngine;
using System.Collections;

public class WeaponZeroTravelForward : DamageSource
{
	private Vector3 displacementVector = Vector3.zero;
	private Rigidbody2D myRigidbody2D = null;
	public float damage = 1f;
	public float speed = 2f;
	public float duration = 1f;
	public bool hitMultipleEnemies = false;
	public bool useTranslate = false;
	
	void Awake()
	{
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	void Start()
	{
		Invoke ("DoTimedDestroy", duration);
	}
	
	void Update()
	{
		if(transform.localScale.x < 0)
		{
			displacementVector = new Vector2 (transform.right.x * -1 * speed, 0f);
			
			if(!useTranslate)
			{
				myRigidbody2D.velocity = displacementVector;
			}
			else
			{
				transform.Translate(displacementVector * Time.deltaTime);
			}
		}
		else
		{
			displacementVector = new Vector2 (transform.right.x * speed, 0f);
			
			if(!useTranslate)
			{
				myRigidbody2D.velocity = displacementVector;
			}
			else
			{
				transform.Translate(displacementVector * Time.deltaTime);
			}
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
			EnemyIntroStageVile introVile = collidingObject.gameObject.GetComponent<EnemyIntroStageVile>();
			Hitpoints collidingObjectHitpoints = collidingObject.GetComponent<Hitpoints> ();
			if(hitMultipleEnemies && introVile != null)
			{
				if(collidingObjectHitpoints != null)
				{
					collidingObjectHitpoints.Damage (damage, gameObject, damageSourceOwner);
					if(damage <= collidingObjectHitpoints.hitpoints)
					{
						Destroy (gameObject);
					}
				}
			}
			else
			{
				if(collidingObjectHitpoints != null)
				{
					collidingObjectHitpoints.Damage (damage, gameObject, damageSourceOwner);
				}
				Destroy (gameObject);
			}
		}
	}
	
	void DoTimedDestroy()
	{
		Destroy (gameObject);
	}
}
