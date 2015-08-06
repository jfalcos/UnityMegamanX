using UnityEngine;
using System.Collections;

public class DamageSource : MonoBehaviour
{
	public GameObject damageSourceOwner = null;
	public LayerMask collidableLayers;

	public bool CanCollideWith(GameObject go)
	{
		int goLayer = 1 << go.layer;
		return (goLayer == collidableLayers.value);
	}
}
