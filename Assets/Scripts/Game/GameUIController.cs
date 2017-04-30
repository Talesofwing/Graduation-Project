using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour {
	
	public DragButton BGMButton;
	public DragButton SFXButton;

	void Start () {
		BaseSetup ();
	}

	void BaseSetup () {

		// 設置音量
		if (SoundManager.Instance.GetBGMVolume () != 0) {
			BGMButton.SetOn ();
		} else {
			BGMButton.SetOff ();
		}

		if (SoundManager.Instance.GetSFXVolume () != 0) {
			SFXButton.SetOn ();
		} else {
			SFXButton.SetOff ();
		}

	}

}
