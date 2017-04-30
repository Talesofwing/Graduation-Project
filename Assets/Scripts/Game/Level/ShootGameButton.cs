using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGameButton : MonoBehaviour {

	private InteractionModel s;			// 交互模型腳本

	void Start () {
		s = GetComponent <InteractionModel> ();
		s.triggerUpEvents += HandleTriggerUp;
		s.triggerDownEvents += HandleTriggerDown;
		s.highlightEvents += HandleHighlight;
	}

	private void HandleTriggerUp (SteamVR_TrackedObject hand) {
		HandController handController = hand.GetComponent <HandController> ();
		handController.ClearEvents ();
//		handController.triggerUpEvents = s.triggerUpEvents;
		ShootGameController.Instance.StartGame ();
	}

	private void HandleTriggerDown (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayPressedSound ();
	}

	private void HandleHighlight (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayHighlightSound ();
	}

}
