using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager gameManager = null;
	private bool paused = false;
	public delegate void DelegateOnGamePaused();
	public delegate void DelegateOnGameUnPaused();
	public DelegateOnGamePaused onGamePaused;
	public DelegateOnGameUnPaused onGameUnPaused;

	void Awake()
	{
		GameObject.DontDestroyOnLoad (gameObject);
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
	
	public void OnLevelStart()
	{
		PlayerPrefs.SetInt ("level", Application.loadedLevel);
	}
	
	public void RestartLevel()
	{
		int lastLoadedLevel = PlayerPrefs.GetInt ("level");
		Application.LoadLevel (lastLoadedLevel);
	}

	public void TogglePause()
	{
		paused = !paused;
		if(!paused)
		{
			onGameUnPaused();
		}
		else
		{
			onGamePaused();
		}
	}

	public static GameManager instance
	{
		get
		{
			return gameManager;
		}
	}
}