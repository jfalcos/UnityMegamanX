using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPanelWeaponStatus : MonoBehaviour
{
	public Image icon = null;
	public Image healthbar = null;
	public MegamanWeapon weapon = null;

	void OnEnable()
	{
		if(weapon != null)
		{
			gameObject.SetActive(weapon.unlocked);
			SetProgress(weapon.energyPercent);
		}
	}

	public void SetProgress(float percent)
	{
		healthbar.fillAmount = percent / 100f;
	}

	public float GetProgressPercent()
	{
		return healthbar.fillAmount * 100;
	}
}