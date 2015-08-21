using UnityEngine;
using System.Collections;

public class IgnorePhysics2DCollisions : MonoBehaviour
{
	public Collider2D[] ignoreList = null;
	public Collider2D[] ignoreAgainst = null;

	void Start()
	{
		foreach(Collider2D c2D in ignoreList)
		{
			foreach(Collider2D localC2D in ignoreAgainst)
			{
				Physics2D.IgnoreCollision(c2D, localC2D);
			}
		}
	}
}
