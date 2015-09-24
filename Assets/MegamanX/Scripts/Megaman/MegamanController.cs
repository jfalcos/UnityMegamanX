using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof(MegamanWeaponManager))]
[RequireComponent (typeof(Jump2D))]
[RequireComponent (typeof(WallJump2D))]
public class MegamanController : MonoBehaviour {

	private Hitpoints hitpoints = null;
	private MegamanInput playerInput = null;
	private MegamanWeaponManager weaponManager = null;
	private Jump2D myJump2D = null;
	private WallJump2D myWallJump2D = null;
	private Rigidbody2D _myRigidbody2D = null;
	private Collider2D _myCollider2D = null;
	public Animator myAnimator = null;
	public GroundCheck groundCheck = null;
	public WallCheck[] wallCheck = null;
	public float speed = 5.0f;
	public ParticleSystem chargingFX = null;
	public ParticleSystem[] dieFX = null;
	public PauseMenuManager pauseMenuManager = null;

	void Awake()
	{
		if(myAnimator == null)
		{
			Debug.LogError(gameObject + " animator is null. Please set the reference.");
		}
		weaponManager = GetComponent<MegamanWeaponManager> ();
		hitpoints = GetComponent<Hitpoints> ();
		_myRigidbody2D = GetComponent<Rigidbody2D> ();
		_myCollider2D = GetComponent<Collider2D> ();
		myJump2D = GetComponent<Jump2D> ();
		myWallJump2D = GetComponent<WallJump2D> ();
		playerInput = GetComponent<MegamanInput> ();
	}

	void Start ()
	{
	}

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
		myCollider2D.enabled = false;
		myRigidbody2D.gravityScale = 0f;
		playerInput.enabled = false;
		myAnimator.SetTrigger ("die");
		myRigidbody2D.velocity = Vector2.zero;
		StartCoroutine (PlayDeathFX ());
	}

	private IEnumerator PlayDeathFX()
	{
		foreach(ParticleSystem ps in dieFX)
		{
			ps.gameObject.SetActive(true);
			ps.Play();
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds (3f);
		GameManager.instance.RestartLevel ();
		yield return null;
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
			_myRigidbody2D.velocity = new Vector2 (axisSpeed * speed, _myRigidbody2D.velocity.y);
			if(axisSpeed == 0)
			{
				axisSpeed = 0.01f;
			}
			myAnimator.SetFloat ("hSpeed", Mathf.Abs(axisSpeed));
//		}
	}

	public void Jump()
	{
		myJump2D.Jump (myAnimator, myRigidbody2D, groundCheck);
		myWallJump2D.WallJump (myAnimator, myRigidbody2D, wallCheck);
	}

	public void FireFX()
	{
		myAnimator.SetBool ("charging", true);
		chargingFX.gameObject.SetActive (true);
	}

	public void Fire(float chargeTime)
	{
		weaponManager.weapon (gameObject, chargeTime);
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
		weaponManager.weapon = weapon;
	}

	public void CaptureBy(GameObject localGameObject)
	{
		_myRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		myAnimator.Play ("idle");
		_myRigidbody2D.gravityScale = 0f;
		MegamanInput playerInput = GameObject.FindObjectOfType<MegamanInput> ();
		playerInput.enabled = false;
		myAnimator.SetFloat ("hSpeed", 0f);
		myAnimator.SetBool ("charging", false);
		myAnimator.SetBool ("receiveDamage", false);
		chargingFX.gameObject.SetActive (false);
		StopFire ();
		DisableWeaponFireAnimation ();
		EnableBentOnKneeAndHurt ();
	}

	public void TeleportOut()
	{
		myAnimator.SetTrigger ("teleportOut");
	}

	public void EnableBentOnKneeAndHurt()
	{
		myAnimator.SetBool ("capturedHurt", true);
		myAnimator.SetTrigger ("capture");
	}

	public void OnGrabbedByEnemy(Vector3 grabPosition)
	{
		myAnimator.enabled = false;
		myAnimator.enabled = true;
		myAnimator.Play ("idle");
		transform.position = grabPosition;
		myAnimator.SetBool ("capturedHurt", false);
		myAnimator.SetTrigger ("grabbed");
	}

	private void DisableWeaponFireAnimation()
	{
		myAnimator.SetBool ("weaponFire", false);
	}

	public float health
	{
		get
		{
			return hitpoints.hitpoints;
		}
	}

	public float healthInPercentage
	{
		get
		{
			return hitpoints.GetRemainingHealthPercentage();
		}
	}
	
	public Rigidbody2D myRigidbody2D
	{
		get
		{
			return _myRigidbody2D;
		}
	}

	public Collider2D myCollider2D
	{
		get
		{
			return _myCollider2D;
		}
	}
}
