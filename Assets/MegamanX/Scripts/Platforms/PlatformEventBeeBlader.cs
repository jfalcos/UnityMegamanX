using UnityEngine;
using System.Collections;

public class PlatformEventBeeBlader : PlatformEvent
{
	private Rigidbody2D myRigidbody2D = null;
	public EnemyBeeBlader beeBlader = null;

	void Awake()
	{
		if(beeBlader == null)
		{
			Debug.LogError(gameObject + " is not configured.");
		}
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	void OnEnable()
	{
		beeBlader.hitpoints.onKill += OnKill;
	}

	void OnDisable()
	{
		beeBlader.hitpoints.onKill -= OnKill;
	}

	void OnKill(Hitpoints hitpoints)
	{
		IntroStageCamera2DFollow cameraManager = GameObject.FindObjectOfType<IntroStageCamera2DFollow>();
		myRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		cameraManager.EnableDefaultMode ();
	}
}
