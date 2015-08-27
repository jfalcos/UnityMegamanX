using UnityEngine;
using System.Collections;

public class EnemyRoadAttacker : Enemy
{
	private Vector3 displacementVector = Vector3.zero;
	public float speed = 1f;
	public float damage = 1f;

	void Update()
	{
		if(transform.localScale.x < 0)
		{
			displacementVector = new Vector2 (transform.right.x * -1 * speed, 0f);
			transform.Translate(displacementVector * Time.deltaTime);
		}
		else
		{
			displacementVector = new Vector2 (transform.right.x * speed, 0f);
			transform.Translate(displacementVector * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D localCollider2D)
	{
		MegamanController megamanController = localCollider2D.gameObject.GetComponent<MegamanController> ();
		if(megamanController != null)
		{
			Hitpoints hitpoints = megamanController.gameObject.GetComponent<Hitpoints>();
			hitpoints.Damage(damage, gameObject, gameObject);
			Invoke ("RotateMeOnX", 1.5f);
		}
	}

	void RotateMeOnX()
	{
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;

		transform.localScale = newScale;
	}
}
