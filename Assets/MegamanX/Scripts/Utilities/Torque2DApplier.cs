using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class Torque2DApplier : MonoBehaviour
{
	private Rigidbody2D myRigidbody2D = null;
	public float torque = 2.5f;
	public bool once = true;
	public bool everyFrame = false;

	void Awake()
	{
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	void Start()
	{
		if(once)
		{
			myRigidbody2D.AddTorque(torque);
		}
	}

	void Update()
	{
		if(everyFrame)
		{
			myRigidbody2D.AddTorque(torque);
		}
	}
}
