using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonGameController : MonoBehaviour {
	public static BalloonGameController Instance;

	public int balloonCount = 10;

	public GameObject balloonPrefab;

	public Text scoreText;

	private int currentBalloonCount = 0;
	private int score = 0;

	void Awake () {
		Instance = this;
	}

	void Start () {

		do {
			CreateBalloons ();
		} while (currentBalloonCount < balloonCount);

	}

	void CreateBalloons () {
		currentBalloonCount++;
		Instantiate (balloonPrefab);
	}

	public void BrokenBalloon (int score) {
		this.score += score;
		scoreText.text = this.score + "";
		currentBalloonCount--;
		CreateBalloons ();
	}

}
