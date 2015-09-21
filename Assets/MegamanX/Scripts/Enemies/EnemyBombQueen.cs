using UnityEngine;
using System.Collections;

public class EnemyBombQueen : Enemy
{
	private Rigidbody2D myRigidbody2D = null;
	private Vector2 moveVector = Vector2.zero;
	private bool canMove = true;
	public GameObject bombBullet = null;
	public float damage = 1f;
	public float speed = 1f;
	public bool moveLeft = true;
	public float detectionRadius = 1f;
	public bool canDropBomb = true;
	private bool canStartDroppingBombs = true;

	protected override void Awake()
	{
		base.Awake ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	protected override void Start()
	{
		base.Start ();
	}

	void Update()
	{
		if(canStartDroppingBombs)
		{
			Collider2D[] allOverlaps = Physics2D.OverlapCircleAll (new Vector2 (transform.position.x, transform.position.y), detectionRadius, collidableLayers);
			if(allOverlaps != null)
			{
				foreach(Collider2D c2D in allOverlaps)
				{
					canStartDroppingBombs = false;
					StartCoroutine(StartDroppingBombs(c2D.gameObject));
					break;
				}
			}
		}
	}

	void FixedUpdate()
	{
		if(canMove)
		{
			VelocityMove(moveLeft, speed, Time.fixedDeltaTime, moveVector, myRigidbody2D);
		}
	}

	void OnTriggerEnter2D(Collider2D localCollision2D)
	{
		Damage (localCollision2D.gameObject, damage);
	}
	
	IEnumerator StartDroppingBombs(GameObject localGameObject)
	{
		canMove = false;
		DropBomb (localGameObject, 150f);
		yield return new WaitForSeconds (0.5f);
		canDropBomb = true;
		DropBomb (localGameObject, 100f);
		yield return new WaitForSeconds (0.5f);
		canDropBomb = true;
		DropBomb (localGameObject, 50f);
		canMove = true;
		StartCoroutine (FlyAway ());
	}
	
	void DropBomb(GameObject localGameObject, float force)
	{
		if(localGameObject != null)
		{
			if(canDropBomb)
			{
				canDropBomb = false;
				MegamanController megaman = localGameObject.GetComponent<MegamanController> ();
				
				if(megaman != null)
				{
					GameObject bombInstance = Instantiate(bombBullet, bombBullet.transform.position, bombBullet.transform.rotation) as GameObject;
					Vector3 moveVector = localGameObject.transform.position - bombInstance.transform.position;
					Rigidbody2D bombRigidbody = bombInstance.GetComponent<Rigidbody2D>();
					bombInstance.SetActive(true);
					bombRigidbody.AddForce(new Vector2(moveVector.x, moveVector.y) * force);
				}
			}
		}
	}

	IEnumerator FlyAway()
	{
		yield return new WaitForSeconds (2f);
		canMove = false;
		bool reachedPosition = false;
		Vector3 goalPosition = transform.position;
		goalPosition.y -= 5f;
		while(!reachedPosition)
		{
			Vector3 translateVector = transform.position - goalPosition;
			transform.Translate(translateVector * speed * Time.deltaTime);
			reachedPosition = ProximityCheck(goalPosition, 0.01f);
			yield return new WaitForSeconds(0.03f);
		}
		Destroy (gameObject);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere (transform.position, detectionRadius);
	}
}
