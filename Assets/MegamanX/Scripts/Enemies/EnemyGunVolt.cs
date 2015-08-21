using UnityEngine;
using System.Collections;

public class EnemyGunVolt : Enemy
{
	private Rigidbody2D myRigidbody2D = null;
	private GameObject bulletInstance = null;
	public Animator animator = null;
	public float attackDelay = 1f;
	public GameObject energyBullet = null;
	public GameObject rocketBullet = null;

	protected override void Awake()
	{
		base.Awake ();
		if(animator == null)
		{
			Debug.LogError(gameObject + " animator is null. Please set the reference.");
		}
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	void Start ()
	{
		hitpoints.onDamage = OnDamage;
		InvokeRepeating ("Attack", attackDelay, attackDelay);
	}

	void Update ()
	{
	}

	void OnDamage(Hitpoints hitpoints)
	{
	}

	void Attack()
	{
		if(bulletInstance == null)
		{
			PlayAttackAnimation ();
		}
	}

	void PlayAttackAnimation()
	{
		if(bulletInstance == null)
		{
			animator.SetBool("attack", true);
		}
	}

	void StopAttackAnimation()
	{
		animator.SetBool ("attack", false);
	}

	public void ChooseAttackTypeAndShoot()
	{
		if(bulletInstance == null)
		{
			float random = Random.Range (0, 100);

			if(random < 50)
			{
				FireEnergyBullet();
			}
			else
			{
				FireRocketBullet();
			}
		}

		StopAttackAnimation ();
	}

	void FireEnergyBullet()
	{
		bulletInstance = Instantiate (energyBullet, energyBullet.transform.position, energyBullet.transform.rotation) as GameObject;
		bulletInstance.SetActive(true);
	}

	void FireRocketBullet()
	{
		bulletInstance = Instantiate (rocketBullet, rocketBullet.transform.position, rocketBullet.transform.rotation) as GameObject;
		bulletInstance.SetActive (true);
	}
}
