using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController Instance;

	public AudioClip menuBGM;
	public AudioClip gameBGM;

	void Awake () {

		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (Instance.gameObject);
		} else {
			Destroy (gameObject);
		}

	}

	void Start () {
		ManagerSetup ();
		GameSetup ();

		SoundManager.Instance.PlayBGM (menuBGM);
	}

	void ManagerSetup () {
		SaveManager.Instance.Setup ();
		SoundManager.Instance.Setup ();
	}

	void GameSetup () {

	}

	public void GotoGame () {
		SoundManager.Instance.StopBGM ();
		SteamVR_LoadLevel.Begin ("game");

		StartCoroutine (WaitForLoading (gameBGM));
	}

	public void GotoMenu () {
		SoundManager.Instance.StopBGM ();
		SteamVR_LoadLevel.Begin ("menu");

		StartCoroutine (WaitForLoading (menuBGM));
	}

	IEnumerator WaitForLoading (AudioClip bgmClip) {

		while (SteamVR_LoadLevel.loading) {
			yield return null;
		}

		SoundManager.Instance.PlayBGM (bgmClip);
	}

}
