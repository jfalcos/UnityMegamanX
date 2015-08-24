using UnityEngine;
using System;
using System.Collections;

public class MegamanController : MonoBehaviour {

	private Hitpoints hitpoints = null;
	private Action<GameObject, float> weapon = null;
	private Rigidbody2D myRigidbody2D = null;
	public Animator myAnimator = null;
	public MegamanGroundCheck groundCheck = null;
	public MegamanWallCheck[] wallCheck = null;
	public float speed = 5.0f;
	public float jumpForce = 400f;
	public ParticleSystem chargingFX = null;

	void Awake()
	{
		if(myAnimator == null)
		{
			Debug.LogError(gameObject + " animator is null. Please set the reference.");
		}
		hitpoints = GetComponent<Hitpoints> ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void FixedUpdate()
	{
	}
	
	void OnEnable()
	{
		hitpoints.onDamage += OnDamage;
		hitpoints.onKill += OnKill;
	}
	
	void OnDisable()
	{
		hitpoints.onDamage -= OnDamage;
		hitpoints.onKill -= OnKill;
	}

	private void OnDamage(Hitpoints hitpoints)
	{
		myAnimator.SetBool ("receiveDamage", true);
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

//		if(groundCheck.grounded)
//		{
			myRigidbody2D.velocity = new Vector2 (axisSpeed * speed, myRigidbody2D.velocity.y);
			if(axisSpeed == 0)
			{
				axisSpeed = 0.01f;
			}
			myAnimator.SetFloat ("hSpeed", Mathf.Abs(axisSpeed));
//		}
	}

	public void Jump()
	{
		if(groundCheck.grounded)
		{
			if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("megaman-jump"))
			{
				myAnimator.SetTrigger("jump");
				myRigidbody2D.AddForce (new Vector2 (0f, jumpForce));
			}
		}
		else
		{
			foreach(MegamanWallCheck localWallCheck in wallCheck)
			{
				if(localWallCheck.walled)
				{
					myRigidbody2D.AddForce(new Vector2(0f, myRigidbody2D.velocity.y + 60f));
					if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("megaman-wall jump"))
					{
						myAnimator.SetTrigger("wallJump");
						break;
					}
				}
			}
		}
	}

	public void FireFX()
	{
		myAnimator.SetBool ("charging", true);
		chargingFX.gameObject.SetActive (true);
	}

	public void Fire(float chargeTime)
	{
		weapon (gameObject, chargeTime);
		myAnimator.SetBool ("weapon", true);
	}

	public void StopFire()
	{
		myAnimator.SetBool ("weaponFire", true);
		myAnimator.SetBool ("charging", false);
		myAnimator.SetBool ("weapon", false);
		chargingFX.gameObject.SetActive (false);
		Invoke ("DisableWeaponFireAnimation", 0.2f);
	}

	public void SetWeapon(Action<GameObject,float> weapon)
	{
		this.weapon = weapon;
	}

	private void DisableWeaponFireAnimation()
	{
		myAnimator.SetBool ("weaponFire", false);
	}
}
