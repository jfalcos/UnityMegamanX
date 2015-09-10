using UnityEngine;
using System.Collections;

public class WeaponVilesEnergyBall : HomingProjectileWeapon
{
	protected override void Awake()
	{
		target = (GameObject.FindObjectOfType<MegamanController> ()).transform;
		damageSourceOwner = (GameObject.FindObjectOfType<EnemyIntroStageVile> ()).gameObject;
	}

	protected override void OnTriggerEnter2D (Collider2D localCollider2D)
	{
		if(target != null)
		{
			if(CanCollideWith(localCollider2D.gameObject))
			{
				MegamanController megamanController = GameObject.FindObjectOfType<MegamanController>();
				megamanController.CaptureBy(damageSourceOwner);
				EnemyIntroStageVile vile = damageSourceOwner.GetComponent<EnemyIntroStageVile>();
				vile.OnCaptureMegaman();
				Destroy (gameObject);
			}
		}
	}
}
