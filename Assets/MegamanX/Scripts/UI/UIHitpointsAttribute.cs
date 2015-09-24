using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHitpointsAttribute : MonoBehaviour
{
	public Hitpoints hitpoints = null;
	public Image fillBar = null;
	
	void OnEnable()
	{
		if(hitpoints != null)
		{
			gameObject.SetActive(hitpoints.gameObject.activeSelf);
			if(fillBar != null)
			{
				SetProgress(hitpoints.GetRemainingHealthPercentage());
			}
			
		}
	}
	
	void SetProgress(float percent)
	{
		fillBar.fillAmount = percent / 100;
	}
	
	public float GetProgressPercent()
	{
		return fillBar.fillAmount * 100;
	}
}
