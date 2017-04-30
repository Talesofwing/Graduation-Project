using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour {
	public Text text;

	public Color normal = Color.white;
	public Color highlight = Color.red;
	public Color pressed = Color.green;

	private InteractionModel s;			// 交互模型腳本

	void Start () {
		s = GetComponent <InteractionModel> ();
		s.triggerUpEvents += HandleTriggerUp;
		s.triggerDownEvents += HandleTriggerDown;
		s.highlightEvents += HandleHighlight;
		s.unHighlightEvents += HandleUnHighlight;
	}

	private void HandleTriggerUp (SteamVR_TrackedObject hand) {
		text.color = highlight;
		GameController.Instance.GotoGame ();
	}

	private void HandleTriggerDown (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayPressedSound ();
		text.color = pressed;
	}

	private void HandleHighlight (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayHighlightSound ();
		text.color = highlight;
	}

	private void HandleUnHighlight (SteamVR_TrackedObject hand) {
		text.color = normal;
	}

}
