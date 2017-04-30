using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
	NONE = 0,
	UI = 1,
	BALLROOM = 2,
	GUNROOM = 3
};

public class Player : MonoBehaviour {
	public static Player PLAYER;

	public HandController rightHand;
	public HandController leftHand;

	void Awake () {
		PLAYER = this;
	}

	public void SetGameState (SteamVR_TrackedObject hand, GameState state) {
		HandController handController = hand.GetComponent <HandController> ();
		handController.SetGameState (state);
	}

}