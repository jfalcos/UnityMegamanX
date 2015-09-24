using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGameAttribute : MonoBehaviour
{
	public GameAttribute gameAttribute = null;
	public Image fillBar = null;
	public Text text = null;

	void OnEnable()
	{
		if(gameAttribute != null)
		{
			gameObject.SetActive(gameAttribute.unlocked);
			if(fillBar != null)
			{
				SetProgress(gameAttribute.valuePercent);
			}
			else if(text != null)
			{
				SetValue(gameAttribute.valueReal);
			}

		}
	}

	void SetProgress(float percent)
	{
		fillBar.fillAmount = percent / 100;
	}

	void SetValue(float myValue)
	{
		text.text = myValue.ToString ();
	}

	public float GetProgressPercent()
	{
		return fillBar.fillAmount * 100;
	}
}