using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MegamanController))]
public class MegamanInput : MonoBehaviour
{
	//Get a reference to the class that can give orders to megaman.
	private MegamanController megamanController = null;
	private float timestamp = 0f;
	private bool timestampSet = false;
	public bool manualLock = false;

	void Awake()
	{
		megamanController = GetComponent<MegamanController> ();
	}

	void Start()
	{
	}

	void Update()
	{
		if(!manualLock)
		{
			megamanController.Move (Input.GetAxis ("Horizontal"));

			if(Input.GetButtonDown("Fire1"))
			{
				if(!timestampSet)
				{
					timestampSet = true;
					timestamp = Time.time;
					megamanController.FireFX ();
				}
			}
			else if(Input.GetButtonUp("Fire1"))
			{
				timestamp = Time.time - timestamp; //used to charge the equipped weapon.
				megamanController.Fire(timestamp);
				timestampSet = false;
				timestamp = 0f;
				megamanController.StopFire();
			}
			else if(Input.GetButtonUp("Pause"))
			{
				GameManager.instance.TogglePause();
			}
		}
	}

	void FixedUpdate()
	{
		if(!manualLock)
		{
			if(Input.GetButton("Jump"))
			{
				megamanController.Jump();
			}
		}
	}
}
