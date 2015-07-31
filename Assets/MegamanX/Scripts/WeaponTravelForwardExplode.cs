using UnityEngine;
using System.Collections;

public class WeaponTravelForwardExplode : MonoBehaviour
{
	private Rigidbody2D rigidbody2D = null;
//	public Animator animator = null;
	public float damage = 1f;
	public float speed = 2f;

	void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D> ();
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
		Destroy (gameObject);
	}
}
