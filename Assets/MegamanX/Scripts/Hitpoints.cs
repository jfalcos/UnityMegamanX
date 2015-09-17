using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Hitpoints : MonoBehaviour
{
	private GameObject _damageSource = null;
	private GameObject _damageSourceOwner = null;
	private float startingHitpoints = 0f;
	public delegate void DelegateOnDamage(Hitpoints hitpoints);
	public delegate void DelegateOnKill(Hitpoints hitpoints);
	public DelegateOnDamage onDamage;
	public DelegateOnDamage onKill;
	public float hitpoints = 1f;
	public bool invulnerable = false;

	void Start()
	{
		startingHitpoints = hitpoints;
	}

	public void Damage(float amount, GameObject source, GameObject sourceOwner)
	{
		_damageSource = source;
		_damageSourceOwner = sourceOwner;

		if(onDamage != null)
		{
			onDamage(this);
		}

		if(!invulnerable)
		{
			hitpoints -= amount;
		}

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

	public float GetRemainingHealthPercentage()
	{
		return (hitpoints * 100) / startingHitpoints;
	}
}
