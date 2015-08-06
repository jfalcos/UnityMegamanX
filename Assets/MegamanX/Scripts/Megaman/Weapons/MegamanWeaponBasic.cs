using UnityEngine;
using System.Collections;

public class MegamanWeaponBasic : MegamanWeapon
{
	public GameObject fxYellowWeapon = null;
	public GameObject fxGreenWeapon = null;
	public GameObject fxBlueWeapon = null;

	public float yellowWeaponChargeTime = 0f;
	public float greenWeaponChargeTime = 0.5f;
	public float blueWeaponChargeTime = 1.0f;

	public override void Fire (GameObject owner, float chargeTime)
	{
		GameObject localWeapon = null;

		if(chargeTime < greenWeaponChargeTime)
		{
			localWeapon = Instantiate (fxYellowWeapon, spawnPoint.position, transform.rotation) as GameObject;
		}
		else if(chargeTime >= greenWeaponChargeTime && chargeTime < blueWeaponChargeTime)
		{
			localWeapon = Instantiate (fxGreenWeapon, spawnPoint.position, transform.rotation) as GameObject;
		}
		else if(chargeTime >= blueWeaponChargeTime)
		{
			localWeapon = Instantiate (fxBlueWeapon, spawnPoint.position, transform.rotation) as GameObject;
		}

		localWeapon.transform.localScale = new Vector3 (owner.transform.localScale.x, localWeapon.transform.localScale.y, localWeapon.transform.localScale.z);
		localWeapon.SetActive (true);
	} 
}
