using UnityEngine;
using System.Collections;

public class GameAttribute : MonoBehaviour
{
	public string attributeName = "";
	private float startingValue = 1f;
	[SerializeField] private float attributeValue = 1f;
	public bool unlocked = false;
	public bool persistent = false;
	public bool reset = false;

	void Start()
	{
		Init ();
	}

	void Init()
	{
		if(persistent)
		{
			if(!PlayerPrefs.HasKey(attributeName) || reset)
			{
				Store ();
				reset = false;
			}
			else
			{
				LoadPersistent();
			}
			startingValue = attributeValue;
		}
	}

	void Store()
	{
		if(attributeName.Length > 0)
		{
			PlayerPrefs.SetFloat(attributeName, attributeValue);
		}
		else
		{
			Debug.LogError("[" + gameObject.name + "] - Could not save persistent value. Attribute name is empty"); 
		}
	}

	void LoadPersistent()
	{
		attributeValue = PlayerPrefs.GetFloat (attributeName);
	}

	public float valueReal
	{
		get
		{
			return attributeValue;
		}
	}

	public float valuePercent
	{
		get
		{
			return (attributeValue * 100) / startingValue;
		}
	}
}
