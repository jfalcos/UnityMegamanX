using UnityEngine;
using System;
using System.Collections;

public class GenericWeaponManager : MonoBehaviour
{
	private Action<GameObject, float> _weapon = null;

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
