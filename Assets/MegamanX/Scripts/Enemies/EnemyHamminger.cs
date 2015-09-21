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

	void OnTriggerEnter2D(Collider2D localCollision2D)
	{
		if(Damage(localCollision2D.gameObject, damage))
		{
			SafeStopCoroutine(flyAwayRoutine);
			collided = true;
			SafeStopCoroutine(moveTowardsPlayer);
			moveTowardsPlayer = StartCoroutine(MoveTowardsPlayer());
		}
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
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds (1f);
		moveTowardsPlayer = StartCoroutine (MoveTowardsPlayer ());
		yield return null;
	}
}
