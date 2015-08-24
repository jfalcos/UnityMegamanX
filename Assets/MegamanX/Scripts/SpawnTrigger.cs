using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnTrigger : MonoBehaviour
{
	private List<GameObject> spawnedEnemies = new List<GameObject> ();
	private bool locked = false;
	public GameObject[] objectsToSpawn = null;
	public bool delayedSpawn = false;
	public float spawnDelay = 0f;

	void OnTriggerEnter2D(Collider2D localCollider2D)
	{
		if(!locked)
		{
			MegamanController megaman = localCollider2D.GetComponent<MegamanController> ();

			if(megaman != null)
			{
				spawnedEnemies.Remove(null);
				if(spawnedEnemies.Count < objectsToSpawn.Length)
				{
					foreach(GameObject go in objectsToSpawn)
					{
						GameObject objectInstance = Instantiate (go, go.transform.position, go.transform.rotation) as GameObject;
						spawnedEnemies.Add(objectInstance);
						objectInstance.SetActive(true);
					}
				}
			}

			if(delayedSpawn)
			{
				locked = true;
				Invoke ("Unlock", spawnDelay);
			}
		}
	}

	void Unlock()
	{
		locked = false;
	}
}
