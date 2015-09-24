using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof(WeaponBeeBlader))]
public class EnemyBeeBlader : Enemy
{
	private WeaponBeeBlader weaponBeeBlader = null;
	private bool moveUp = true;
	private bool moveDown = false;
	private Rigidbody2D myRigidbody2D = null;
	private BoxCollider2D myCollider2D = null;
	private IntroStageCamera2DFollow camera2D = null;
	public float speed = 1f;
	public Transform ballDeVouxSpawnPoint = null;
	public Transform rocketSpawnPoint = null;
	public Transform upPositionReference = null;
	public Transform downPositionReference = null;
	public float attackTimespan = 1f; //seconds
	public SpriteRenderer mySprite = null;
	public Sprite destroyedSprite = null;
	public PolygonCollider2D destroyedCollider = null;
	public PlatformEventBeeBlader platformEvent = null;
	public bool amIBeeBlader1 = false;
	public bool amIBeeBlader2 = false;

	protected override void Awake()
	{
		base.Awake ();
		weaponBeeBlader = GetComponent<WeaponBeeBlader> ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		myCollider2D = GetComponent<BoxCollider2D> ();
		myAnimator = mySprite.gameObject.GetComponent<Animator> ();
		camera2D = GameObject.FindObjectOfType<IntroStageCamera2DFollow> ();
	}

	protected override void Start()
	{
		base.Start ();
		InvokeRepeating ("Attack", attackTimespan, attackTimespan);
	}

	void Update()
	{
		MoveUp ();
		MoveDown ();
	}

	protected override void OnEnable ()
	{
		base.OnEnable ();
		if(platformEvent != null)
		{
			platformEvent.enabled = true;
		}
		if(amIBeeBlader1)
		{
			camera2D.EnableBeeBlader1Mode();
		}
		else if(amIBeeBlader2)
		{
			camera2D.EnableBeeBlader2Mode();
		}
	}

	private void Attack()
	{
		int chance = UnityEngine.Random.Range (0, 100);
		if (chance <= 20)
		{
			GameObject voux = weaponBeeBlader.SpawnBallDeVoux(ballDeVouxSpawnPoint.transform.position, Quaternion.identity);			
			Collider2D vouxCollider2D = voux.GetComponent<Collider2D> ();
			Physics2D.IgnoreCollision (vouxCollider2D, destroyedCollider);
		}
		else if(chance > 20 && chance <= 66)
		{
			weaponBeeBlader.SpawnRocket(rocketSpawnPoint.transform.position, rocketSpawnPoint.transform.rotation);
		}
		else if(chance > 66)
		{
			weaponBeeBlader.MachineGun();
		}
	}

	private void MoveUp()
	{
		if(moveUp)
		{
			float yDelta = Mathf.Lerp(transform.position.y, upPositionReference.transform.position.y, Time.deltaTime * speed);
			transform.position = new Vector3(transform.position.x, yDelta, transform.position.z);
			if(transform.position.y >= (upPositionReference.transform.position.y - 0.1f))
			{
				moveUp = false;
				moveDown = true;
			}
		}
	}

	private void MoveDown()
	{
		if(moveDown)
		{
			if(transform != null)
			{
				float yDelta = Mathf.Lerp(transform.position.y, downPositionReference.transform.position.y, Time.deltaTime * speed);
				transform.position = new Vector3(transform.position.x, yDelta, transform.position.z);
				if(transform.position.y <= (downPositionReference.transform.position.y + 0.1f))
				{
					moveUp = true;
					moveDown = false;
				}
			}
		}
	}

	protected override void DestroyMe ()
	{
		if(onDestroyFX != null)
		{
			GameObject localGameObject = Instantiate(onDestroyFX, onDestroyFX.transform.position, onDestroyFX.transform.rotation) as GameObject;
			localGameObject.SetActive(true);
		}
		moveDown = false;
		moveUp = false;
		myCollider2D.enabled = false;
		destroyedCollider.enabled = true;
		myAnimator.enabled = false;
		mySprite.sprite = destroyedSprite;
		myRigidbody2D.gravityScale = 1f;
		CancelInvoke ();
		this.enabled = false;
	}
}