using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Image))]
public class ImgHealthbar : MonoBehaviour
{
	public Image img = null;
	public Hitpoints hitpoints = null;

	void Awake()
	{
		img = GetComponent<Image> ();
	}

	void OnEnable()
	{
		hitpoints.onDamage += OnDamage;
	}

	void OnDisable()
	{
		hitpoints.onDamage -= OnDamage;
	}

	public void OnDamage(Hitpoints hitpoints)
	{
		img.fillAmount = hitpoints.GetRemainingHealthPercentage () / 100;
	}
}
