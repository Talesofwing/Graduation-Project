using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HandEventHandle (SteamVR_TrackedObject hand);
public delegate void GameEventHandle ();

public class HandController : MonoBehaviour {

	public GameObject ball;
	public Transform balllSpawn;
	public Gun gun;
	public GameObject handModel;

	private GameObject currentBall;

	private GameState state = GameState.NONE;
	public GameState State {
		get { return state; }
	}
	private GameState preState = GameState.NONE;


	private SteamVR_TrackedController handController;
	private SteamVR_TrackedObject hand;
	public SteamVR_TrackedObject Hand {
		get { return hand; }
	}


	public event HandEventHandle triggerDownEvents;
	public event HandEventHandle triggerUpEvents;

	private event GameEventHandle triggerDownGameEvents;
	private event GameEventHandle triggerUpGameEvents;

	void Start () {
		Setup ();

		handController.TriggerClicked += HandleTriggerDown;
		handController.TriggerUnclicked += HandleTriggerUp;
	}
		
	void Setup () {
		handController = GetComponent <SteamVR_TrackedController> ();
		hand = GetComponent <SteamVR_TrackedObject> ();
	}

	private void HandleTriggerDown (object sender, ClickedEventArgs e) {
		
		if (triggerDownEvents != null)
			triggerDownEvents (hand);

		if (triggerDownGameEvents != null)
			triggerDownGameEvents ();

	}

	private void HandleTriggerUp (object sender, ClickedEventArgs e) {

		if (triggerUpEvents != null)
			triggerUpEvents (hand);

		if (triggerUpGameEvents != null)
			triggerUpGameEvents (); 

	}

	public void SetGameStateByPreGameState () {
		GameState temp = preState;
		preState = state;
		state = temp;

		UpdateState ();
	}

	public void SetGameState (GameState state) {
		preState = this.state;
		this.state = state;

		UpdateState ();
	}

	void UpdateState () {
		ClearGameEvents ();
		RemoveAllGameState ();

		switch (state) {
		case GameState.NONE:
			
			break;
		case GameState.UI:

			break;
		case GameState.BALLROOM:
			triggerDownGameEvents += HandleBallRoomTriggerDownEvent;
			triggerUpGameEvents += HandleBallRoomTriggerUpEvent;
			break;
		case GameState.GUNROOM:
			gun.gameObject.SetActive (true);
			handModel.SetActive (false);
			triggerDownGameEvents += HandleGunRoomTriggerDownEvent;
			triggerUpGameEvents += HandleGunRoomTriggerUpEvent;
			break;
		}

	}
		
	void RemoveAllGameState () {
		Destroy (currentBall);
		gun.gameObject.SetActive (false);

		handModel.SetActive (true);
	}

	void ClearGameEvents () {
		triggerDownGameEvents = null;
		triggerUpGameEvents = null;
	}

	public void ClearEvents () {
		triggerDownEvents = null;
		triggerUpEvents = null;
	}

	private bool isDown = false;
	private Vector3 downPos;
	private float downTime;
	void HandleBallRoomTriggerDownEvent () {
		if (currentBall) {
			isDown = true;
			downPos = transform.position;
			downTime = Time.time;
			return;
		}

		// 生成球
		currentBall = Instantiate (ball);
		currentBall.transform.SetParent (balllSpawn);
		currentBall.transform.localPosition = Vector3.zero;
		// 隨機顏色
		Color color = ColorHelper.GetRandomColor ();
		currentBall.GetComponent <MeshRenderer> ().material.color = color;
		currentBall.GetComponent <MeshRenderer> ().material.SetColor ("_EmissionColor", color);
	}

	void HandleBallRoomTriggerUpEvent () {
		
		if (isDown) {
			Vector3 upPos = transform.position;
			float upTime = Time.time;
			Vector3 distance = upPos - downPos;
			float duration = upTime - downTime;
			Vector3 force = distance / duration * 4;
			currentBall.GetComponent <Rigidbody> ().isKinematic = false;
			currentBall.transform.SetParent (null);
//			currentBall.GetComponent <Rigidbody> ().AddForce (force * 5f);
			currentBall.GetComponent <Rigidbody> ().velocity = force;
			Destroy (currentBall, 20f);
			currentBall = null;
			isDown = false;
		}


	}

	void HandleGunRoomTriggerDownEvent () {
		gun.Fire ();
	}

	void HandleGunRoomTriggerUpEvent () {

	}

}
