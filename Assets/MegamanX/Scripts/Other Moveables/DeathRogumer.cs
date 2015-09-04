using UnityEngine;
using System.Collections;

[RequireComponent (typeof(DeathRogumerMovement))]
public class DeathRogumer : MonoBehaviour
{
	private MegamanController megamanController = null;
	private DeathRogumerMovement deathRogumerMovement = null;
	public Animator liftAnimator = null;
	public GameObject roadAttackerPrefab = null;
	public float liftDelay = 0f;

	void Awake()
	{
		megamanController = GameObject.FindObjectOfType<MegamanController> ();
		deathRogumerMovement = GetComponent<DeathRogumerMovement> ();
		if(liftAnimator == null)
		{
			Debug.LogError("The Lift Animator is null. Please set the reference for it to work properly.");
		}
	}

	void Start()
	{
		deathRogumerMovement.enabled = false;
		IntroStageCamera2DFollow camera2D = GameObject.FindObjectOfType<IntroStageCamera2DFollow> ();
		camera2D.EnableDeathRogumerMode ();
		InvokeRepeating ("DeployLift", liftDelay, liftDelay);
	}

	void Update()
	{
	}

	public void EnableManualMovement()
	{
		deathRogumerMovement.enabled = true;
	}

	void DeployLift()
	{
		liftAnimator.SetTrigger ("deploy");
	}

	public void DeployRoadAttacker()
	{
		GameObject roadAttacker = Instantiate (roadAttackerPrefab, roadAttackerPrefab.transform.position, roadAttackerPrefab.transform.rotation) as GameObject;
		roadAttacker.SetActive (true);
	}
}
