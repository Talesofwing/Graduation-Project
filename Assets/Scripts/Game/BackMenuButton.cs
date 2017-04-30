using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMenuButton : MonoBehaviour {


	private InteractionModel s;			// 交互模型腳本

	void Start () {
		s = GetComponent <InteractionModel> ();
		s.triggerUpEvents += HandleTriggerUp;
		s.triggerDownEvents += HandleTriggerDown;
		s.highlightEvents += HandleHighlight;
		s.unHighlightEvents += HandleUnHighlight;
	}

	private void HandleTriggerUp (SteamVR_TrackedObject hand) {
		GameController.Instance.GotoMenu ();
	}

	private void HandleTriggerDown (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayPressedSound ();
	}

	private void HandleHighlight (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayHighlightSound ();
	}

	private void HandleUnHighlight (SteamVR_TrackedObject hand) {

	}

}
