using UnityEngine;
using System.Collections;

public class EnemyHamminger : Enemy
{
	private Vector3 translateVector = Vector3.zero;
	public float speed = 2f;
	public float damage = 1f;
	public MegamanController target = null;

	void Start()
	{
		target = GameObject.FindObjectOfType<MegamanController> ();
	}

	void FixedUpdate()
	{
		translateVector = target.transform.position - transform.position;
		transform.Translate (translateVector * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D localCollision2D)
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
