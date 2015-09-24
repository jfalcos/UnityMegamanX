using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MegamanController))]
public class MegamanWeaponManager : GenericWeaponManager
{
	private MegamanWeapon[] weapons = null;
	private int currentActiveWeapon = 0;

	void Awake()
	{
		weapons = GetComponents<MegamanWeapon> ();
	}

	void Start()
	{
		UpdateCurrentActiveWeapon ();
	}

	public MegamanWeapon Next()
	{
		weapons [currentActiveWeapon].enabled = false;
		currentActiveWeapon++;

		if(currentActiveWeapon >= weapons.Length)
		{
			currentActiveWeapon = 0;
		}

		weapons [currentActiveWeapon].enabled = true;
		return weapons [currentActiveWeapon];
	}

	void UpdateCurrentActiveWeapon()
	{
		for(int a = 0; a < weapons.Length; ++a)
		{
			if(weapons[a].enabled)
			{
				currentActiveWeapon = a;
				break;
			}
		}
	}
}
