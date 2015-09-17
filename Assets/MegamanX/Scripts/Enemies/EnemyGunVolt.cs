using UnityEngine;
using System.Collections;

public class EnemyGunVolt : Enemy
{
	private GameObject bulletInstance = null;
	public float attackDelay = 1f;
	public GameObject energyBullet = null;
	public GameObject rocketBullet = null;

	protected override void Awake()
	{
		base.Awake ();
		if(myAnimator == null)
		{
			Debug.LogError(gameObject + " animator is null. Please set the reference.");
		}
	}

	protected override void Start ()
	{
		base.Start ();
		InvokeRepeating ("Attack", attackDelay, attackDelay);
	}

	void Update ()
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
			myAnimator.SetBool("attack", true);
		}
	}

	void StopAttackAnimation()
	{
		myAnimator.SetBool ("attack", false);
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
