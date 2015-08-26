using UnityEngine;
using System.Collections;

public class WeaponBeeBlader : MonoBehaviour
{
	public GameObject ballDeVouxPrefab = null;
	public GameObject rocketPrefab = null;

	public GameObject SpawnBallDeVoux(Vector3 myPosition, Quaternion myRotation)
	{
		GameObject voux = Instantiate(ballDeVouxPrefab.gameObject, myPosition, myRotation) as GameObject;
		return voux;
	}

	public GameObject SpawnRocket(Vector3 myPosition, Quaternion myRotation)
	{
		GameObject spawnedWeapon = Instantiate(rocketPrefab.gameObject, myPosition, myRotation) as GameObject;
		return spawnedWeapon;
	}

	public GameObject MachineGun()
	{
		return null;
	}
	/*

		GameObject voux = Instantiate(ballDeVouxPrefab.gameObject, ballDeVouxSpawnPoint.transform.position, Quaternion.identity) as GameObject;
		Collider2D vouxCollider2D = voux.GetComponent<Collider2D> ();
		Physics2D.IgnoreCollision (vouxCollider2D, destroyedCollider);
	 */
}
