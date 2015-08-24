using UnityEngine;
using System.Collections;

public class PlatformEventBeeBlader : PlatformEvent
{
	private Hitpoints beeBladerHitpoints = null;
	private Rigidbody2D myRigidbody2D = null;
	public EnemyBeeBlader beeBlader = null;

	void Awake()
	{
		if(beeBlader == null)
		{
			Debug.LogError(gameObject + " is not configured.");
		}
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		beeBladerHitpoints = beeBlader.gameObject.GetComponent<Hitpoints> ();
	}

	void OnEnable()
	{
		beeBladerHitpoints.onKill += OnKill;
	}

	void OnDisable()
	{
		beeBladerHitpoints.onKill -= OnKill;
	}

	void OnKill(Hitpoints hitpoints)
	{
		myRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
	}
}
