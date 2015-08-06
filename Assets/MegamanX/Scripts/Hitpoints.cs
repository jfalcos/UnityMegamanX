using UnityEngine;
using System;
using System.Collections;

public class Hitpoints : MonoBehaviour
{
	private GameObject _damageSource = null;
	private GameObject _damageSourceOwner = null;
	public Action<Hitpoints> onKill;
	public Action<Hitpoints> onDamage;
	public float hitpoints = 1f;

	public void Damage(float amount, GameObject source, GameObject sourceOwner)
	{
		_damageSource = source;
		_damageSourceOwner = sourceOwner;

		if(onDamage != null)
		{
			onDamage(this);
		}

		hitpoints -= amount;

		if(hitpoints <= 0f)
		{
			if(onKill != null)
			{
				onKill(this);
			}
			else
			{
				//Default destroy behavior
				Destroy (gameObject);
			}
		}
	}

	public GameObject damageSource
	{
		get
		{
			return _damageSource;
		}
	}

	public GameObject damageSourceOwner
	{
		get
		{
			return _damageSourceOwner;
		}
	}
}
