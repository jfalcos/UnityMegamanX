using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{
	public bool _grounded = false;
	public LayerMask groundLayer;

	void OnTriggerStay2D(Collider2D collider2D)
	{
		if(!_grounded)
		{
			OnTriggerEnter2D(collider2D);
		}
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if(IsGameObjectInGroundLayer(collider2D.gameObject))
		{
			_grounded = true;
		}
		else
		{
			_grounded = false;
		}
	}

	void OnTriggerExit2D(Collider2D collider2D)
	{
		if(IsGameObjectInGroundLayer(collider2D.gameObject))
		{
			_grounded = false;
		}
	}

	private bool IsGameObjectInGroundLayer(GameObject go)
	{
		int goLayer = 1 << go.layer;
		bool success = false;
		
		if(goLayer == groundLayer.value)
		{
			success = true;
		}
		else
		{
			success = false;
		}

		return success;
	}

	public bool grounded
	{
		get
		{
			return _grounded;
		}
	}
}
