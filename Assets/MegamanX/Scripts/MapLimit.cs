using UnityEngine;
using System.Collections;

public class MapLimit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D localCollider2D)
	{
		if(!localCollider2D.gameObject.Equals(gameObject))
		{
			Hitpoints hitpoints = localCollider2D.gameObject.GetComponent<Hitpoints> ();
			if(hitpoints != null)
			{
				hitpoints.Damage(1000000f, gameObject, gameObject);
			}
			else
			{
				Destroy (localCollider2D.gameObject);
			}
		}
	}
}
