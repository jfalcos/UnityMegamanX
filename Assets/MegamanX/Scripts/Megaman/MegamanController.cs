using UnityEngine;
using System;
using System.Collections;

public class MegamanController : MonoBehaviour {

	private Hitpoints hitpoints = null;
	private Action<GameObject, float> weapon = null;
	private Rigidbody2D myRigidbody2D = null;
	public Animator animator = null;
	public MegamanGroundCheck groundCheck = null;
	public float speed = 5.0f;
	public float jumpForce = 400f;
	public ParticleSystem chargingFX = null;

	void Awake()
	{
		if(animator == null)
		{
			Debug.LogError(gameObject + " animator is null. Please set the reference.");
		}
		hitpoints = GetComponent<Hitpoints> ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		hitpoints.onDamage = OnDamage;
		hitpoints.onKill = OnKill;	
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void FixedUpdate()
	{
	}

	private void OnDamage(Hitpoints hitpoints)
	{
		animator.SetBool ("receiveDamage", true);
	}

	private void OnKill(Hitpoints hitpoints)
	{
	}

	private void RotateToDirection(float horizontalAxis)
	{
		Vector3 myScale = transform.localScale;
		bool rotate = false;

		if(horizontalAxis < 0 && myScale.x > 0)
		{
			rotate = true;
		}
		else if(horizontalAxis > 0 && myScale.x < 0)
		{
			rotate = true;
		}

		if(rotate)
		{
			myScale.x = myScale.x * -1;
			transform.localScale = myScale;
		}
	}

	public void Move(float axisSpeed)
	{
		RotateToDirection (axisSpeed);

		if(groundCheck.grounded)
		{
			myRigidbody2D.velocity = new Vector2 (axisSpeed * speed, myRigidbody2D.velocity.y);
			if(axisSpeed == 0)
			{
				axisSpeed = 0.01f;
			}
			animator.SetFloat ("hSpeed", Mathf.Abs(axisSpeed));
		}
	}

	public void Jump()
	{
		if(groundCheck.grounded)
		{
			myRigidbody2D.AddForce (new Vector2 (0f, jumpForce));
		}
	}

	public void FireFX()
	{
		animator.SetBool ("charging", true);
		chargingFX.gameObject.SetActive (true);
	}

	public void Fire(float chargeTime)
	{
		weapon (gameObject, chargeTime);
		animator.SetBool ("weapon", true);
	}

	public void StopFire()
	{
		animator.SetBool ("weaponFire", true);
		animator.SetBool ("charging", false);
		animator.SetBool ("weapon", false);
		chargingFX.gameObject.SetActive (false);
		Invoke ("DisableWeaponFireAnimation", 0.2f);
	}

	public void SetWeapon(Action<GameObject,float> weapon)
	{
		this.weapon = weapon;
	}

	private void DisableWeaponFireAnimation()
	{
		animator.SetBool ("weaponFire", false);
	}
}
