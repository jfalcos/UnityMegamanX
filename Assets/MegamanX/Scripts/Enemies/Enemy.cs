using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Hitpoints))]
public class Enemy : DamageSource
{
	protected Hitpoints hitpoints = null;
	public GameObject onDestroyFX = null;

	protected virtual void Awake()
	{
		hitpoints = GetComponent<Hitpoints> ();
	}

	protected virtual void OnEnable()
	{
		print (gameObject);
		hitpoints.onKill += OnKill;
	}

	protected virtual void OnDisable()
	{
		hitpoints.onKill -= OnKill;
	}

	protected virtual void OnKill(Hitpoints hitpoints)
	{
		DestroyMe ();
	}

	protected virtual void DestroyMe()
	{
		if(onDestroyFX != null)
		{
			GameObject localGameObject = Instantiate(onDestroyFX, onDestroyFX.transform.position, onDestroyFX.transform.rotation) as GameObject;
			localGameObject.SetActive(true);
		}
		Destroy (gameObject);
	}
}
