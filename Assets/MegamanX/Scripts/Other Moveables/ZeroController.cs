using UnityEngine;
using System.Collections;

public class ZeroController : MonoBehaviour
{
	public GameObject energyWeapon = null;
	public Transform energyWeaponSpawnMarker = null;

	public float speed = 1f;

	public Animator myAnimator = null;

	public bool testTriggerAnimation = false;
	public string testTriggerName = "";

	public bool testBoolAnimation = false;
	public string testBoolName = "";

	void Update()
	{
		if(testTriggerAnimation)
		{
			testTriggerAnimation = false;
			myAnimator.SetTrigger(testTriggerName);
		}

		if(testBoolAnimation)
		{
			testBoolAnimation = false;
			myAnimator.SetTrigger(testBoolName);
		}
	}

	public void Aim()
	{
		myAnimator.SetTrigger ("aim");
		myAnimator.SetBool ("continueAiming", true);
	}

	public void StopAiming()
	{
		myAnimator.SetBool ("continueAiming", false);
	}

	public void Charge()
	{
		myAnimator.SetTrigger ("charge");
		myAnimator.SetBool ("continueCharging", true);
	}
	
	public void StopCharging()
	{
		myAnimator.SetBool ("continueCharging", false);
	}

	public void Slide()
	{
		myAnimator.SetTrigger ("slide");
		myAnimator.SetBool ("continueSliding", true);
	}

	public void StopSliding()
	{
		myAnimator.SetBool ("continueSliding", false);
	}

	public void Talk()
	{
		myAnimator.SetBool ("continueTalking", true);
		myAnimator.SetTrigger ("talk");
	}

	public void StopTalking()
	{
		myAnimator.SetBool ("continueTalking", false);
	}

	public void LookUp()
	{
		myAnimator.SetTrigger ("lookUp");
	}

	public void Teleport()
	{
		myAnimator.SetTrigger ("teleport");
	}

	public void Fire()
	{
		GameObject localGameObject = Instantiate (energyWeapon, energyWeaponSpawnMarker.position, energyWeapon.transform.rotation) as GameObject;
		localGameObject.SetActive (true);
	}
}
