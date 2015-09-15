using UnityEngine;
using System.Collections;

public class WallCheck : MonoBehaviour
{
	public bool _walled = false;
	public LayerMask wallLayer;
	
	void OnTriggerStay2D(Collider2D collider2D)
	{
		OnTriggerEnter2D(collider2D);
	}
	
	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if(IsGameObjectInWallLayer(collider2D.gameObject))
		{
			_walled = true;
		}
		else
		{
			_walled = false;
		}
	}
	
	void OnTriggerExit2D(Collider2D collider2D)
	{
		if(IsGameObjectInWallLayer(collider2D.gameObject))
		{
			_walled = false;
		}
	}
	
	private bool IsGameObjectInWallLayer(GameObject go)
	{
		int goLayer = 1 << go.layer;
		bool success = false;
		
		if(goLayer == wallLayer.value)
		{
			success = true;
		}
		else
		{
			success = false;
		}
		
		return success;
	}
	
	public bool walled
	{
		get
		{
			return _walled;
		}
	}
}
