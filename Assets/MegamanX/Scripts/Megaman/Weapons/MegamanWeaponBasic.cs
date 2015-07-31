using UnityEngine;
using System.Collections;

public class MegamanWeaponBasic : MegamanWeapon
{
	public GameObject fxYellowWeapon = null;
	public GameObject fxGreenWeapon = null;
	public GameObject fxBlueWeapon = null;

	public override void Fire (float chargeTime)
	{
		Instantiate (fxGreenWeapon, spawnPoint.position, transform.rotation);
	} 
}
