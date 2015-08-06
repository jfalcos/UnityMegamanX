using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MegamanController))]
public class MegamanWeapon : MonoBehaviour
{
	private MegamanController megamanController = null;
	public Transform spawnPoint = null;

	protected virtual void Awake()
	{
		megamanController = GetComponent<MegamanController> ();
	}

	public virtual void Fire(GameObject owner, float chargeTime)
	{
		Debug.Log ("Fire - MegamanWeapon");
	}

	protected virtual void OnEnable()
	{
		megamanController.SetWeapon (Fire);
	}
}
