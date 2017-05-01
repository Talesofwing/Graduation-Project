using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip pressedSound;
	public AudioClip highlightSound;

	private static SoundManager _instance;

	[SerializeField] private AudioSource SFX;
	[SerializeField] private AudioSource BGM;

	private float BGMVolume = 0.5f;
	private float SFXVolume = 0.4f;

	private const float kBGMVolume = 0.5f;
	private const float kSFXVolume = 0.4f;

	public static SoundManager Instance {
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
		// 設置音量
		BGMVolume = SaveManager.Instance.GetBGMVolume ();
		SFXVolume = SaveManager.Instance.GetSFXVolume ();
		BGM.volume = BGMVolume;
		SFX.volume = SFXVolume;
	}

	public void PlayBGM (AudioClip clip, bool repeat = true) {
		BGM.loop = repeat;
		BGM.clip = clip;
		BGM.Play ();
	}

	public void StopBGM () {
		BGM.Stop ();
	}

	public void PlaySFX (AudioClip clip) {
		SFX.PlayOneShot (clip, SFX.volume);
	}

	public void PlaySFX (AudioClip clip, float volume) {
		if (SFX.volume == 0) return;

		SFX.PlayOneShot (clip, volume);
	}

	public void PlayPressedSound () {
		PlaySFX (pressedSound);
	}

	public void PlayHighlightSound () {
		PlaySFX (highlightSound);
	}

	public void SetBGMVolume () {
		BGMVolume = BGMVolume > 0 ? 0 : kBGMVolume;
		BGM.volume = BGMVolume;
		SaveManager.Instance.SetBGMVolume (BGMVolume);
	}

	public void SetSFXVolume () {
		SFXVolume = SFXVolume > 0 ? 0 : kSFXVolume;
		SFX.volume = SFXVolume;
		SaveManager.Instance.SetSFXVolume (SFXVolume);
	}

	public void SetBGMVolume (bool isOn) {

		if (isOn) {
			BGMVolume = kBGMVolume;
		} else {
			BGMVolume = 0f;
		}

		BGM.volume = BGMVolume;
		SaveManager.Instance.SetBGMVolume (BGMVolume);
	}

	public void SetSFXVolume (bool isOn) {
		if (isOn) {
			SFXVolume = kSFXVolume;
		} else {
			SFXVolume = 0f;
		}

		SFX.volume = SFXVolume;
		SaveManager.Instance.SetSFXVolume (SFXVolume);
	}

	public float GetBGMVolume () {
		return BGMVolume;
	}

	public float GetSFXVolume () {
		return SFXVolume;
	}

}
