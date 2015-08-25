using UnityEngine;
using System.Collections;

public class WeaponBombQueenMine : MonoBehaviour
{
	private Rigidbody2D myRigidbody2D = null;
	public Animator myAnimator = null;
	public float damage = 1f;
	public float explosionDelay = 2f;
	public float explosionRadius = 0.2f;

	void Awake()
	{
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	void Start()
	{
	}

	void OnCollisionEnter2D(Collision2D localCollider2D)
	{
		if(!myAnimator.GetBool("armed"))
		{
			myAnimator.SetBool("armed", true);
			myRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
			Invoke ("Explode", explosionDelay);
		}
	}

	void Explode()
	{
		Collider2D[] allColliders = Physics2D.OverlapCircleAll (transform.position, explosionRadius);
		foreach(Collider2D collider2D in allColliders)
		{
			MegamanController megamanController = collider2D.gameObject.GetComponent<MegamanController>();
			if(megamanController != null)
			{
				Hitpoints hitpoints = megamanController.GetComponent<Hitpoints>();
				if(hitpoints != null)
				{
					hitpoints.Damage(damage, gameObject, null);
				}
			}
		}
		Destroy (gameObject);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}
