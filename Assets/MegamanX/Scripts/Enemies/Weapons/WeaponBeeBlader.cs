using UnityEngine;
using System.Collections;

[RequireComponent (typeof(WeaponBeeBladerMachineGun))]
public class WeaponBeeBlader : MonoBehaviour
{
	private WeaponBeeBladerMachineGun machineGun = null;
	public GameObject ballDeVouxPrefab = null;
	public GameObject rocketPrefab = null;

	void Awake()
	{
		machineGun = GetComponent<WeaponBeeBladerMachineGun> ();
	}

	public GameObject SpawnBallDeVoux(Vector3 myPosition, Quaternion myRotation)
	{
		machineGun.enabled = false;
		GameObject voux = Instantiate(ballDeVouxPrefab.gameObject, myPosition, myRotation) as GameObject;
		return voux;
	}

	public GameObject SpawnRocket(Vector3 myPosition, Quaternion myRotation)
	{
		machineGun.enabled = false;
		GameObject spawnedWeapon = Instantiate(rocketPrefab.gameObject, myPosition, myRotation) as GameObject;
		return spawnedWeapon;
	}

	public void MachineGun()
	{
		machineGun.enabled = true;
	}
}
