using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroStageZerosCutscene : MonoBehaviour
{
	private MegamanController megamanController = null;
	private EnemyIntroStageVile vile = null;
	private DeathRogumer deathRogumer = null;
	public ZeroController zeroController = null;
	public bool playing = false;
	public bool started = false;
	public RectTransform chatbox = null;
	public RectTransform zerosMugShot = null;
	public RectTransform megamansMugShot = null;
	public RectTransform vilesMugShot = null;
	public RectTransform vilesDialog = null;
	public RectTransform[] megamansDialogs = null;
	public RectTransform[] zerosDialogs = null;

	void Awake()
	{
	}

	void Start()
	{
	}

	void Update()
	{
		if(!started && playing)
		{
			StartCoroutine(BeginCutscene());
			started = true;
		}
	}

	public IEnumerator BeginCutscene()
	{
		megamanController = GameObject.FindObjectOfType<MegamanController> ();
		vile = GameObject.FindObjectOfType<EnemyIntroStageVile> ();
		deathRogumer = GameObject.FindObjectOfType<DeathRogumer> ();
		zeroController.gameObject.SetActive (true);
		zeroController.transform.position = new Vector3 (megamanController.transform.position.x - 4f, zeroController.transform.position.y, zeroController.transform.position.z);
		IntroStageCamera2DFollow camera2D = GameObject.FindObjectOfType<IntroStageCamera2DFollow> ();
		camera2D.enabled = false;
		bool loop = false;		
		started = true;
		playing = true;

		VileTalks ();
		vilesDialog.gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		DisableDialogs ();
		chatbox.gameObject.SetActive (false);
		zeroController.Fire ();
		yield return new WaitForSeconds (0.7f);
		vile.BreakArm ();
		megamanController.myRigidbody2D.gravityScale = 1f;
		megamanController.EnableBentOnKneeAndHurt ();
		yield return new WaitForSeconds (1f);
		zeroController.Slide ();
		//Move zero
		loop = true;
		while(loop)
		{
			zeroController.transform.Translate (((megamanController.transform.position + Vector3.right * 2f) - zeroController.transform.position) * zeroController.speed * Time.deltaTime);
			if((zeroController.transform.position.x - megamanController.transform.position.x) > 0.6f && Vector3.Distance(zeroController.transform.position, megamanController.transform.position) < 0.8f)
			{
				zeroController.StopSliding();
				loop = false;
			}
			yield return new WaitForSeconds(Time.deltaTime);
		}
		//
		zeroController.Aim ();
		yield return new WaitForSeconds (1f);
		zeroController.StopAiming ();
		zeroController.Charge ();
		deathRogumer.transform.position = new Vector3 (vile.transform.position.x, deathRogumer.transform.position.y, deathRogumer.transform.position.z);
		deathRogumer.Deploy ();
		yield return new WaitForSeconds (1f);
		deathRogumer.DeployLift ();
		yield return new WaitForSeconds (1f);
		vile.jumpForce.y *= 2;
		vile.SetIgnoreLiftCollision (true);
		vile.JumpUp ();
		zeroController.StopCharging ();
		zeroController.Fire ();
		yield return new WaitForSeconds (0.5f);
		zeroController.LookUp ();
		vile.SetIgnoreLiftCollision (false);
		yield return new WaitForSeconds (0.5f);
		deathRogumer.ReadyToLeaveScene ();
		yield return new WaitForSeconds (1f);
		zeroController.transform.localScale = new Vector3 (zeroController.transform.localScale.x * -1, zeroController.transform.localScale.y, zeroController.transform.localScale.z);
		megamanController.transform.localScale = new Vector3 (1, megamanController.transform.localScale.y, megamanController.transform.localScale.z);
		yield return new WaitForSeconds (1f);
		MegamanTalks ();
		DisableDialogs ();
		megamansDialogs [0].gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		ZeroTalks ();
		DisableDialogs ();
		zerosDialogs [0].gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		ZeroTalks ();
		DisableDialogs ();
		zerosDialogs [1].gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		ZeroTalks ();
		DisableDialogs ();
		zerosDialogs [2].gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		ZeroTalks ();
		DisableDialogs ();
		zerosDialogs [3].gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		ZeroTalks ();
		DisableDialogs ();
		zerosDialogs [4].gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		ZeroTalks ();
		DisableDialogs ();
		zerosDialogs [5].gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		ZeroTalks ();
		DisableDialogs ();
		zerosDialogs [6].gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		chatbox.gameObject.SetActive (false);
		yield return new WaitForSeconds (1f);
		zeroController.Teleport ();
		yield return new WaitForSeconds (1f);
		megamanController.TeleportOut ();
		yield return null;
	}

	public void ZeroTalks()
	{
		chatbox.gameObject.SetActive (true);
		zerosMugShot.gameObject.SetActive (true);
		vilesMugShot.gameObject.SetActive (false);
		megamansMugShot.gameObject.SetActive (false);
		zeroController.Talk ();
	}

	public void MegamanTalks()
	{
		chatbox.gameObject.SetActive (true);
		zeroController.StopTalking ();
		vilesMugShot.gameObject.SetActive (false);
		zerosMugShot.gameObject.SetActive (false);
		megamansMugShot.gameObject.SetActive (true);
	}

	public void VileTalks()
	{
		chatbox.gameObject.SetActive (true);
		zeroController.StopTalking ();
		vilesMugShot.gameObject.SetActive (true);
		zerosMugShot.gameObject.SetActive (false);
		megamansMugShot.gameObject.SetActive (false);
	}

	public void DisableDialogs()
	{
		foreach(RectTransform dialog in zerosDialogs)
		{
			dialog.gameObject.SetActive(false);
		}

		foreach(RectTransform dialog in megamansDialogs)
		{
			dialog.gameObject.SetActive(false);
		}

		vilesDialog.gameObject.SetActive (false);
	}
}

/*
1. Energy weapon is fired.
2. Vile's robot arm breaks.
3. Zero slides to the scene.
4. Vile's ship comes down.
5. Vile's ship pulls the lift down.
6. Vile jumps to the lift.
7. The lift pulls up.
8. The ship leaves.
 */