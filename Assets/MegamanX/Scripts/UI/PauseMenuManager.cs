using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
	private Image myImage = null;
	public RectTransform[] uiToEnableDisable = null;
	public UIPanelWeaponStatus busterWeaponStatus = null,
							   tornadoWeaponStatus = null,
							   torpedoWeaponStatus = null,
							   sparkWeaponStatus = null,
							   stingWeaponStatus = null,
							   cutterWeaponStatus = null,
							   shieldWeaponStatus = null,
							   iceWeaponStatus = null,
							   fireWallWeaponStatus = null,
							   escapeWeaponStatus = null;

	void Awake()
	{
		myImage = GetComponent<Image> ();
	}

	void OnEnable()
	{
		GameManager.instance.onGamePaused += EnableUI;
		GameManager.instance.onGameUnPaused += DisableUI;
	}

	void OnDisable()
	{
		GameManager.instance.onGamePaused -= EnableUI;
		GameManager.instance.onGameUnPaused -= DisableUI;
	}

	void EnableUI()
	{
		myImage.enabled = true;
		foreach(RectTransform myTransform in uiToEnableDisable)
		{
			myTransform.gameObject.SetActive(true);
		}
	}

	void DisableUI()
	{
		myImage.enabled = false;
		foreach(RectTransform myTransform in uiToEnableDisable)
		{
			myTransform.gameObject.SetActive(false);
		}
	}

	public void SetBusterEnergy(float percent)
	{
		busterWeaponStatus.SetProgress (percent);
	}

	public void SetTornadoEnergy(float percent)
	{
	}
}
