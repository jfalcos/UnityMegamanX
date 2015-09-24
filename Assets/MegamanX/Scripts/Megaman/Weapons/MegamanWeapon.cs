using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MegamanController))]
public class MegamanWeapon : MonoBehaviour
{
	private MegamanController megamanController = null;
	private float startingEnergy = 1f;
	public float energy = 1f;
	public Transform spawnPoint = null;
	public bool unlocked = false;
	public bool infiniteAmmo = false;

	protected virtual void Awake()
	{
		megamanController = GetComponent<MegamanController> ();
		startingEnergy = energy;
	}

	protected virtual void OnEnable()
	{
		megamanController.SetWeapon (Fire);
	}
	
	public virtual void Fire(GameObject owner, float chargeTime)
	{
		Debug.Log ("Fire - MegamanWeapon");
	}

	public float energyPercent
	{
		get
		{
			if(!infiniteAmmo)
			{
				return (energy * 100) / startingEnergy;
			}
			else
			{
				return 100;
			}
		}
	}
}
