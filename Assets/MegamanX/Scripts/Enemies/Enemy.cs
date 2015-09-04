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

	protected virtual void Start()
	{
	}

	protected virtual void OnEnable()
	{
		hitpoints.onDamage += OnDamage;
		hitpoints.onKill += OnKill;
	}

	protected virtual void OnDisable()
	{
		hitpoints.onDamage -= OnDamage;
		hitpoints.onKill -= OnKill;
	}

	protected virtual void OnDamage(Hitpoints hitpoints)
	{
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

	protected bool ProximityCheck(MegamanController megamanController, float distance)
	{
		return (Mathf.Abs (Vector3.Distance (transform.position, megamanController.transform.position)) <= distance);
	}

	protected bool ProximityCheck(Vector3 goalPosition, float distance)
	{
		return (Mathf.Abs (Vector3.Distance (transform.position, goalPosition)) <= distance);
	}

	protected bool ProximityCheckAgainstMegamanController(float radius, LayerMask layerMask)
	{
		bool success = false;
		Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask.value);

		foreach(Collider2D localCollider2D in closeColliders)
		{
			MegamanController megamanController = localCollider2D.gameObject.GetComponent<MegamanController>();
			if(megamanController != null)
			{
				success = true;
			}
		}

		return success;
	}

	protected void SafeStopCoroutine(Coroutine routine)
	{
		if(routine != null)
		{
			StopCoroutine(routine);
		}
	}
}
