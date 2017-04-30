using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomUIController : MonoBehaviour {

	private LevelButton levelButton;

	public int gameState = 2;

	void Start () {
		levelButton = GetComponent <LevelButton> ();
		levelButton.upEvents += HandleUpEvent;
	}

	private void HandleUpEvent (SteamVR_TrackedObject hand) {
		Player.PLAYER.SetGameState (hand, (GameState)gameState);
	}

}
