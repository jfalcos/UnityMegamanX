using UnityEngine;
using System.Collections;

public class WeaponBeeBladerMachineGun : MonoBehaviour
{
	public float damage = 1f;
	public GameObject muzzleFlash = null;
	public Transform targetPosition = null;
	public LayerMask megamanLayer;

	void OnEnable()
	{
		StartCoroutine (AttackRoutine ());
		muzzleFlash.SetActive (true);
	}

	void OnDisable()
	{
		StopAllCoroutines ();
		muzzleFlash.SetActive (false);
	}

	IEnumerator AttackRoutine()
	{
		MegamanController megamanController = null;

		while(true)
		{
			RaycastHit2D[] allHits = Physics2D.RaycastAll (transform.position, targetPosition.localPosition, 5f, megamanLayer.value);
			foreach(RaycastHit2D localHit2D in allHits)
			{
				megamanController = localHit2D.collider.gameObject.GetComponent<MegamanController>();
				if(megamanController != null)
				{
					Hitpoints hitpoints = megamanController.gameObject.GetComponent<Hitpoints>();
					hitpoints.Damage(damage, gameObject, gameObject);
					break;
				}
			}
			yield return new WaitForSeconds (0.5f);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawRay (transform.position, targetPosition.localPosition);
	}
}