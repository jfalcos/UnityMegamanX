using UnityEngine;
using System.Collections;

public class EnemyBeeBlader : Enemy
{
	private bool moveUp = true;
	private bool moveDown = false;
	private Rigidbody2D myRigidbody2D = null;
	private BoxCollider2D myCollider2D = null;
	private Animator myAnimator = null;
	public float speed = 1f;
	public EnemyBallDeVoux ballDeVouxPrefab = null;
	public Transform ballDeVouxSpawnPoint = null;
	public Transform upPositionReference = null;
	public Transform downPositionReference = null;
	public float spawnTimespan = 1f; //seconds
	public SpriteRenderer mySprite = null;
	public Sprite destroyedSprite = null;
	public PolygonCollider2D destroyedCollider = null;

	protected override void Awake()
	{
		base.Awake ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		myCollider2D = GetComponent<BoxCollider2D> ();
		myAnimator = mySprite.gameObject.GetComponent<Animator> ();
	}

	void Start()
	{
		InvokeRepeating ("SpawnBallDeVoux", spawnTimespan, spawnTimespan);
	}

	void Update()
	{
		MoveUp ();
		MoveDown ();
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
			float yDelta = Mathf.Lerp(transform.position.y, downPositionReference.transform.position.y, Time.deltaTime * speed);
			transform.position = new Vector3(transform.position.x, yDelta, transform.position.z);
			if(transform.position.y <= (downPositionReference.transform.position.y + 0.1f))
			{
				print ("Switch move down");
				moveUp = true;
				moveDown = false;
			}
		}
	}

	private void SpawnBallDeVoux()
	{
//		float chance = Random.Range (0, 1000);
//		bool spawn = false;
//
//		if(chance <= 10)
//		{
//			spawn = true;
//		}
//
//		if(spawn)
//		{
//			GameObject voux = Instantiate(ballDeVouxPrefab, ballDeVouxSpawnPoint.transform.position, Quaternion.identity) as GameObject;
//		}
		GameObject voux = Instantiate(ballDeVouxPrefab.gameObject, ballDeVouxSpawnPoint.transform.position, Quaternion.identity) as GameObject;
		Collider2D vouxCollider2D = voux.GetComponent<Collider2D> ();
		Physics2D.IgnoreCollision (vouxCollider2D, destroyedCollider);
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