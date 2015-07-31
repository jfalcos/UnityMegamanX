using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MegamanController))]
public class MegamanInput : MonoBehaviour
{
	//Get a reference to the class that can give orders to megaman.
	private MegamanController megamanController = null;
	private float timestamp = 0f;
	private bool timestampSet = false;

	void Awake()
	{
		megamanController = GetComponent<MegamanController> ();
	}

	void Start()
	{
	}

	void Update()
	{
		megamanController.Move (Input.GetAxis ("Horizontal"));

		if(Input.GetButtonDown("Fire1"))
		{
			if(!timestampSet)
			{
				timestampSet = true;
				timestamp = Time.time;
			}
		}
		else if(Input.GetButtonUp("Fire1"))
		{
			megamanController.Fire(timestamp);
		}
		else
		{
			megamanController.StopFire();
		}
	}
}
