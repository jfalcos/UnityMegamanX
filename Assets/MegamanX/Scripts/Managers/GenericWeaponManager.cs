using UnityEngine;
using System;
using System.Collections;

public class GenericWeaponManager : MonoBehaviour
{
	protected Action<GameObject, float> _weapon = null;

	public Action<GameObject, float> weapon
	{
		get
		{
			return _weapon;
		}
		set
		{
			_weapon = value;
		}
	}
}
