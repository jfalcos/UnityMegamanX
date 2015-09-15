using UnityEngine;
using System.Collections;

public class EnemyHamminger : Enemy
{
	private Coroutine flyAwayRoutine = null,
					  moveTowardsPlayer = null,
					  attackPlayer = null,
					  returnToClosestPositionBeforeAttack = null;
	private Vector3 translateVector = Vector3.zero;
	private Vector3 closestPositionBeforeAttack = Vector3.zero;
	private bool collided = false;
	public float speed = 1f;
	public float damage = 1f;
	public float proximityRadius = 0.2f;
	public MegamanController target = null;

	protected override void Start()
	{
		base.Start ();
		target = GameObject.FindObjectOfType<MegamanController> ();
		moveTowardsPlayer = StartCoroutine (MoveTowardsPlayer ());
	}

	void FixedUpdate()
	{
//		translateVector = target.transform.position - transform.position;
//		transform.Translate (translateVector * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D localCollision2D)
	{
		if(CanCollideWith(localCollision2D.gameObject))
		{
			Hitpoints hitpoints = localCollision2D.gameObject.GetComponent<Hitpoints>();
			if(hitpoints != null)
			{
				hitpoints.Damage(damage, gameObject, gameObject);
				collided = true;
				StopCoroutine(flyAwayRoutine);
			}
		}
	}

	IEnumerator FlyAway()
	{
		yield return new WaitForSeconds (3f);
		SafeStopCoroutine (moveTowardsPlayer);
		SafeStopCoroutine (attackPlayer);
		SafeStopCoroutine (returnToClosestPositionBeforeAttack);

		bool distanceReached = false;
		Vector3 flyAwayPosition = transform.position;
		flyAwayPosition.y += 10f;

		while(!distanceReached)
		{
			translateVector = flyAwayPosition - transform.position;
			transform.Translate(translateVector * speed * Time.deltaTime);
			distanceReached = ProximityCheck(flyAwayPosition, 0.1f);
			yield return new WaitForSeconds(0.01f);
		}

		Destroy (gameObject);
	}

	IEnumerator MoveTowardsPlayer()
	{
		bool attackDistanceReached = false;
		flyAwayRoutine = StartCoroutine (FlyAway ());

		while(!attackDistanceReached)
		{
			translateVector = target.transform.position - transform.position;
			transform.Translate (translateVector * speed * Time.deltaTime);
			attackDistanceReached = ProximityCheckAgainstMegamanController(proximityRadius, collidableLayers.value);
			yield return new WaitForSeconds(0.01f);
		}
		closestPositionBeforeAttack = transform.position;
	 	attackPlayer = StartCoroutine (AttackPlayer ());
	}

	IEnumerator AttackPlayer()
	{
		bool finished = false;

		while(!finished)
		{
			translateVector = target.transform.position - transform.position;
			transform.Translate (translateVector * speed * Time.deltaTime);
			if(collided)
			{
				collided  = false;
				finished = true;
			}
			yield return new WaitForSeconds(0.01f);
		}

		returnToClosestPositionBeforeAttack = StartCoroutine (ReturnToClosestPositionBeforeAttack ());
		yield return null;
	}

	IEnumerator ReturnToClosestPositionBeforeAttack()
	{
		bool reachedPosition = false;

		while(!reachedPosition)
		{
			translateVector = closestPositionBeforeAttack - transform.position;
			transform.Translate(translateVector * speed * Time.deltaTime);
			if(ProximityCheck(closestPositionBeforeAttack, 0.0001f))
			{
				reachedPosition = true;
			}
		}
		yield return new WaitForSeconds (1f);
		moveTowardsPlayer = StartCoroutine (MoveTowardsPlayer ());
		yield return null;
	}
}
