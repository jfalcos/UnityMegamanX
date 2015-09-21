using UnityEngine;
using System.Collections;

public class DamageSource : MonoBehaviour
{
	public GameObject damageSourceOwner = null;
	public LayerMask collidableLayers;

	protected Hitpoints Damage(GameObject localGameObject, float damage)
	{
		Hitpoints hitpoints = null;

		if(CanCollideWith(localGameObject))
		{
			hitpoints = localGameObject.GetComponent<Hitpoints>();
			if(hitpoints != null)
			{
				hitpoints.Damage(damage, gameObject, gameObject);
			}
		}
		
		return hitpoints;
	}

	protected Hitpoints Damage (GameObject localGameObject, float damage, GameObject damageSource, GameObject damageSourceOwner)
	{
		Hitpoints hitpoints = null;

		if(CanCollideWith(localGameObject))
		{
			hitpoints = localGameObject.GetComponent<Hitpoints>();
			if(hitpoints != null)
			{
				hitpoints.Damage(damage, damageSource, damageSourceOwner);
			}
		}

		return hitpoints;
	}

	public bool CanCollideWith(GameObject go)
	{
		int goLayer = 1 << go.layer;
		return (goLayer == collidableLayers.value);
	}
}
