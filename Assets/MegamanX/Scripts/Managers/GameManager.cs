using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager gameManager = null;

	void Awake()
	{
		GameObject.DontDestroyOnLoad (gameObject);
	}

	public void OnLevelStart()
	{
		PlayerPrefs.SetInt ("level", Application.loadedLevel);
	}

	public void RestartLevel()
	{
		int lastLoadedLevel = PlayerPrefs.GetInt ("level");
		Application.LoadLevel (lastLoadedLevel);
	}

	void OnEnable()
	{
		if(gameManager == null)
		{
			gameManager = GameObject.FindObjectOfType<GameManager>();
		}
		else
		{
			Destroy(gameObject);
		}
		OnLevelStart ();
	}

	public static GameManager instance
	{
		get
		{
			return gameManager;
		}
	}
}