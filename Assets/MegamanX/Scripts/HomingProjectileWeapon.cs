using UnityEngine;
using System.Collections;

public class HomingProjectileWeapon : DamageSource
{
	protected Vector3 translationVector = Vector3.zero;
	public Transform target = null;
	public float speed = 1f;
	public float damage = 1f;

	protected virtual void Awake()
	{
	}

	protected virtual void FixedUpdate()
	{
		if(target != null)
		{
			translationVector = target.position - transform.position;
			transform.Translate (translationVector * speed * Time.fixedDeltaTime);
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D localCollider2D)
	{
		if(target != null)
		{
			if(Damage(localCollider2D.gameObject, damage) != null)
			{
				Destroy (gameObject);
			}
		}
	}
}