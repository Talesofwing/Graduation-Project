using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

	public Text BGMText;
	public Text SFXText;

	void Start () {
		BaseSetup ();
	}

	void BaseSetup () {

		// 設置音量
		if (SoundManager.Instance.GetBGMVolume () != 0) {
			BGMText.text = "BGM ON";
		} else {
			BGMText.text = "BGM OFF";
		}

		if (SoundManager.Instance.GetSFXVolume () != 0) {
			SFXText.text = "SFX ON";
		} else {
			SFXText.text = "SFX OFF";
		}

	}

}
