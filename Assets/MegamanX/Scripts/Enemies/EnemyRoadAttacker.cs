using UnityEngine;
using System.Collections;

public class EnemyRoadAttacker : Enemy
{
	private Vector3 displacementVector = Vector3.zero;
	private Rigidbody2D myRigidbody2D = null;
	private Collider2D myCollider2D = null;
	private bool madeGroundSwitch = false;
	private uint currentForm = 1;
	public GameObject firstFormGraphics = null;
	public GameObject secondFormGraphics = null;
	public GameObject thirdFormGraphics = null;
	public float secondFormActivationHitpoints = 0f,
				thirdFormActivationHitpoints = 0f;
	public GroundCheck groundCheck = null;
	public GameObject bulletPrefab = null;
	public Transform bulletSpawnPoint = null;
	public float speed = 1f;
	public float damage = 1f;

	protected override void Awake ()
	{
		base.Awake ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		myCollider2D = GetComponent<BoxCollider2D> ();
	}

	protected override void Start()
	{
		base.Start ();
		secondFormGraphics.SetActive (false);
		thirdFormGraphics.SetActive (false);
	}

	void Update()
	{
		MoveGrounded();
		if(!groundCheck.grounded)
		{
			if(!madeGroundSwitch)
			{
				SwitchToNotGrounded();
			}
		}
		else
		{
			if(madeGroundSwitch && currentForm == 1)
			{
				SwitchToGrounded();
			}
		}
	}

	void MoveGrounded()
	{
		if(transform.localScale.x < 0)
		{
			displacementVector = new Vector2 (transform.right.x * -1 * speed, 0f);
			transform.Translate(displacementVector * Time.deltaTime);
		}
		else
		{
			displacementVector = new Vector2 (transform.right.x * speed, 0f);
			transform.Translate(displacementVector * Time.deltaTime);
		}
	}

	void SwitchToNotGrounded()
	{
		madeGroundSwitch = true;
		myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		myCollider2D.isTrigger = false;
	}

	void SwitchToGrounded()
	{
		madeGroundSwitch = false;
		myRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		myCollider2D.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D localCollider2D)
	{
		MegamanController megamanController = localCollider2D.gameObject.GetComponent<MegamanController> ();
		if(megamanController != null)
		{
			Hitpoints hitpoints = megamanController.gameObject.GetComponent<Hitpoints>();
			hitpoints.Damage(damage, gameObject, gameObject);
			if(currentForm == 1)
			{
				if(!IsInvoking("RotateMeOnX"))
				{
					Invoke ("RotateMeOnX", 1.5f);
				}
				if(!IsInvoking("Fire"))
				{
					Invoke ("Fire", 2.0f);
				}
			}
		}
	}

	void RotateMeOnX()
	{
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;

		transform.localScale = newScale;
	}

	void Fire()
	{
		GameObject localGameobject = Instantiate (bulletPrefab, bulletSpawnPoint.position, Quaternion.identity) as GameObject;
		localGameobject.transform.localScale = new Vector3 (transform.localScale.x, bulletPrefab.transform.localScale.y, bulletPrefab.transform.localScale.z);
		localGameobject.SetActive (true);
	}

	protected override void OnDamage (Hitpoints hitpoints)
	{
		base.OnDamage (hitpoints);
		if(hitpoints.hitpoints > thirdFormActivationHitpoints && hitpoints.hitpoints <= secondFormActivationHitpoints)
		{
			ActivateSecondForm();
		}
		else if(hitpoints.hitpoints > 0 && hitpoints.hitpoints <= thirdFormActivationHitpoints)
		{
			ActivateThirdForm();
		}
	}

	private void ActivateSecondForm()
	{
		currentForm = 2;
		myCollider2D.isTrigger = false;
		firstFormGraphics.SetActive (false);
		secondFormGraphics.SetActive (true);
		myAnimator = secondFormGraphics.GetComponent<Animator> ();
	}

	private void ActivateThirdForm()
	{
		currentForm = 3;
		secondFormGraphics.SetActive (false);
		thirdFormGraphics.SetActive (true);
		myAnimator = thirdFormGraphics.GetComponent<Animator> ();
	}
}
