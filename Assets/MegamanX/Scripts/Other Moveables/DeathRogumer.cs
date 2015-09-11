using UnityEngine;
using System.Collections;

[RequireComponent (typeof(DeathRogumerMovement))]
public class DeathRogumer : MonoBehaviour
{
	private MegamanController megamanController = null;
	private DeathRogumerMovement deathRogumerMovement = null;
	private bool _deployingVile = false;
	private int numberOfDeployedRoadAttackers = 0;
	private const uint roadAttackersLimitBeforeVileAppears = 4;
	private Coroutine vilesDeployementRoutine = null;
	public GameObject vile = null; //Reference to vile.
	public Animator myAnimator = null;
	public Animator liftAnimator = null;
	public GameObject roadAttackerPrefab = null;
	public float liftDelay = 0f; //Delay before the lift comes down and spawns a road attacker.
	public GameObject positionMarkerForCutscene = null; //A game object, child of the main camera, with the position the death rogumer should have once Vile's cutscene starts.
	public bool deployVile = false;

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
		if(deployVile)
		{
			deployVile = false;
			StartCoroutine(DeployVile());
		}

		if(numberOfDeployedRoadAttackers >= roadAttackersLimitBeforeVileAppears && vilesDeployementRoutine == null)
		{
			vilesDeployementRoutine = StartCoroutine(DeployVile());
		}
	}

	public void EnableManualMovement()
	{
		deathRogumerMovement.enabled = true;
	}
	
	public void Deploy()
	{
		deathRogumerMovement.enabled = false;
		myAnimator.SetTrigger ("deploy");
	}

	public void DeployLift()
	{
		liftAnimator.SetTrigger ("deploy");
	}

	IEnumerator DeployVile()
	{
		CancelInvoke ();
		MegamanInput playerInput = megamanController.gameObject.GetComponent<MegamanInput> ();
		playerInput.manualLock = true;
		deathRogumerMovement.enabled = false;
		//Move the death rogumer to position
		float distance = Vector3.Distance (transform.position, positionMarkerForCutscene.transform.position);
		Vector3 translationVector = Vector3.zero;
		while(distance > 0.002f)
		{
			distance = Vector3.Distance (transform.position, positionMarkerForCutscene.transform.position) % 1;
			translationVector = positionMarkerForCutscene.transform.position - transform.position;
			translationVector.z = 0f;
			transform.Translate(translationVector * Time.deltaTime);
			yield return new WaitForSeconds(0.03f);
		}
		_deployingVile = true;
		vile.gameObject.SetActive (true);
		DeployLift ();
		yield return null;
	}

	public void DeployRoadAttacker()
	{
		numberOfDeployedRoadAttackers++;
		if(numberOfDeployedRoadAttackers < roadAttackersLimitBeforeVileAppears)
		{
			GameObject roadAttacker = Instantiate (roadAttackerPrefab, roadAttackerPrefab.transform.position, roadAttackerPrefab.transform.rotation) as GameObject;
			roadAttacker.SetActive (true);
		}
		else
		{
			CancelInvoke();
		}
	}

	public void ReadyToLeaveScene()
	{
		StartCoroutine (LeaveScene ());
	}

	IEnumerator LeaveScene()
	{
		Vector3 destinationVector = new Vector3 (transform.position.x, 2.9f, transform.position.z);
		float distance = Vector3.Distance (transform.position, destinationVector);
		Vector3 translationVector = Vector3.zero;
		while(distance > 0.2f)
		{
			distance = Vector3.Distance (transform.position, destinationVector);
			translationVector = destinationVector - transform.position;
			translationVector.z = 0f;
			transform.Translate(translationVector * Time.deltaTime);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		yield return null;
	}

	public bool deployingVile
	{
		get
		{
			return _deployingVile;
		}
	}
}
