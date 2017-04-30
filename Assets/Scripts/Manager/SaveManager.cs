using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
	public static string SAVEBGMVOLUME = "BGMVolume";
	public static string SAVESFXVOLUME = "SFXVolume";
	public static string SAVESHOOTGAMEBESTTIME = "ShootGameBestTime";

	private float bgmVolume;
	private float sfxVolume;
	private float bestTime;

	private static SaveManager _instance;
	public static SaveManager Instance {
		get { return _instance; }
	}

	void Awake () {
		
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (_instance.gameObject);
		} else {
			Destroy (gameObject);
		}

	}

	public void Setup () {
//		PlayerPrefs.DeleteAll ();

		bgmVolume = PlayerPrefs.GetFloat (SAVEBGMVOLUME, 0.7f);
		sfxVolume = PlayerPrefs.GetFloat (SAVESFXVOLUME, 0.3f);
		bestTime = PlayerPrefs.GetFloat (SAVESHOOTGAMEBESTTIME, 0);
	}

	public float GetBGMVolume () {
		return bgmVolume;
	}

	public float GetSFXVolume () {
		return sfxVolume;
	}

	public void SetBGMVolume (float volume) {
		bgmVolume = volume;
		Save ();
	}

	public void SetSFXVolume (float volume) {
		sfxVolume = volume;
		Save ();
	}

	public void SetBestTime (float time) {
		bestTime = time;
		Save ();
	}

	public float GetBestTime () {
		return bestTime;
	}

	public void Save () {
		PlayerPrefs.SetFloat (SAVEBGMVOLUME, bgmVolume);
		PlayerPrefs.SetFloat (SAVESFXVOLUME, sfxVolume);
		PlayerPrefs.SetFloat (SAVESHOOTGAMEBESTTIME, bestTime);
	}

}
