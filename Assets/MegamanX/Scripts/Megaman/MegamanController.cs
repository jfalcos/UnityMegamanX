using UnityEngine;
using System;
using System.Collections;

public class MegamanController : MonoBehaviour {

	private Rigidbody2D rigidbody2D = null;
	public Animator animator = null;
	public float speed = 5.0f;
	private Action<float> weapon = null;

	void Awake()
	{
		if(animator == null)
		{
			Debug.LogError(gameObject + " animator is null. Please set the reference.");
		}
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}

	public void Move(float axisSpeed)
	{
		rigidbody2D.velocity = new Vector2 (axisSpeed * speed, rigidbody2D.velocity.y);
		if(axisSpeed == 0)
		{
			axisSpeed = 0.01f;
		}
		animator.SetFloat ("hSpeed", Mathf.Abs(axisSpeed));
	}

	public void Fire(float chargeTime)
	{
		weapon (chargeTime);
		animator.SetBool ("weapon", true);
	}

	public void StopFire()
	{
		animator.SetBool ("weapon", false);
	}

	public void SetWeapon(Action<float> weapon)
	{
		this.weapon = weapon;
	}
}
