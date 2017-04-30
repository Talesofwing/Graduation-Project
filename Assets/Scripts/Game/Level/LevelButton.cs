using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LevelButtonEventHandle (SteamVR_TrackedObject hand);

public class LevelButton : MonoBehaviour {

	private InteractionModel s;			// 交互模型腳本

	public event LevelButtonEventHandle upEvents;

	void Start () {
		s = GetComponent <InteractionModel> ();
		s.triggerUpEvents += HandleTriggerUp;
		s.triggerDownEvents += HandleTriggerDown;
		s.highlightEvents += HandleHighlight;

	}

	private void HandleTriggerUp (SteamVR_TrackedObject hand) {
		HandleUnHighlight (hand);
		s.unHighlightEvents -= HandleUnHighlight;

		if (upEvents != null)
			upEvents (hand);

	}

	private void HandleTriggerDown (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayPressedSound ();
	}

	private void HandleHighlight (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayHighlightSound ();
		hand.GetComponent <HandController> ().SetGameState (GameState.UI);
		s.unHighlightEvents += HandleUnHighlight;
	}

	private void HandleUnHighlight (SteamVR_TrackedObject hand) {
		hand.GetComponent <HandController> ().SetGameStateByPreGameState ();
	}

}
