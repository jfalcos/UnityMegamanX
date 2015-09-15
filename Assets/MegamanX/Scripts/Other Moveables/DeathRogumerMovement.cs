using UnityEngine;
using System.Collections;

public class DeathRogumerMovement : MonoBehaviour
{
	private MegamanController megamanController = null;
	private bool pickedDirection = false;
	private Vector3 startPosition = Vector3.zero;
	private Vector3 goalPosition = Vector3.zero;
	private float startTime = 0f;
	private float journeyLength = 0f;
	public float speed = 1f;
	public Transform leftPositionMarker = null;
	public Transform rightPositionMarker = null;

	void Awake()
	{
		megamanController = GameObject.FindObjectOfType<MegamanController> ();
	}

	void Update()
	{
		if(!pickedDirection)
		{
			PickDirection();
		}
		else
		{
			Move ();
			ReachGoalPosition();
		}
	}

	void PickDirection()
	{
		// postiive = move right
		// negative = move left
		Vector3 positionReferenceToMegaman = transform.position - megamanController.transform.position;

		if(positionReferenceToMegaman.x > 0)
		{
			goalPosition = rightPositionMarker.transform.position;
		}
		else
		{
			goalPosition = leftPositionMarker.transform.position;
		}

		startTime = Time.time;
		startPosition = transform.position;
		pickedDirection = true;
		journeyLength = Vector3.Distance (startPosition, goalPosition);
	}

	void Move()
	{
		float distanceCovered = (Time.time - startTime) * speed;
		float fractionJourney = distanceCovered / journeyLength;
		transform.position = Vector3.Lerp (startPosition, goalPosition, fractionJourney);
	}

	void ReachGoalPosition()
	{
		float distance = Mathf.Abs(Vector3.Distance (goalPosition, transform.position));
		if(distance <= 0)
		{
			pickedDirection = false;
		}
	}
}
