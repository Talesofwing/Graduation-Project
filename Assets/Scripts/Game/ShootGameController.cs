using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootGameController : MonoBehaviour {
	public static ShootGameController Instance;

	public Text clockText;
	public float clock = 3f;
	private float clockTime;

	public GameObject[] targetPrefabs;
	public GameObject[] startGameButton;

	public Transform[] targetPosition;

	public Text timeText;
	private float time;
	private int count;

	void Awake () {
		Instance = this;
	}

	void Start () {
		clockText.text = "";

		ShowBestTime ();
	}

	private bool inGame = false;
	void Update () {

		if (inGame) {
			time += Time.deltaTime;
			SetTimeText (time);
		}

	}

	public void StartGame () {
		if (inGame)
			return;

		for (int i = 0; i < startGameButton.Length; i++) {
			startGameButton [i].SetActive (false);
		}

		StartClock ();
	}

	void StartClock () {
		StartCoroutine (Clock ());
	}

	IEnumerator Clock () {
		
		while (clockTime < clock + 1) {
			clockTime += Time.deltaTime;
			clockText.text = "" + (clock - (int)clockTime);

			if (clockTime >= clock) {
				clockText.text = "GO";
			}

			yield return null;
		}

		clockText.text = "";

		ClearBullets ();

		timeText.color = Color.white;
		count = targetPosition.Length;

		for (int i = 0; i < count; i++) {
			CreateTarget (i);
		}

		inGame = true;
		clockTime = 0;
	}

	void ClearBullets () {
		GameObject[] bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		GameObject[] balls = GameObject.FindGameObjectsWithTag ("Ball");
		foreach (GameObject bullet in bullets) {
			Destroy (bullet);
		}

		foreach (GameObject ball in balls) {
			Destroy (ball);
		}
	}

	public void BrokenTarget () {
		count--;

		if (count <= 0) {
			GameOver ();
		}

	}

	void GameOver () {
		inGame = false;
		float bestTime = SaveManager.Instance.GetBestTime ();

		if (time < bestTime || bestTime == 0) {
			timeText.text = "挑戰成功!";
			SaveManager.Instance.SetBestTime (time);
		} else {
			timeText.text = "挑戰失敗!";
		}

		StartCoroutine(DelayShowBestTime ());
		time = 0f;
	}

	IEnumerator DelayShowBestTime () {
		yield return new WaitForSeconds (2f);

		ShowBestTime ();

		for (int i = 0; i < startGameButton.Length; i++) {
			startGameButton [i].SetActive (true);
		}

	}

	void ShowBestTime () {
		float bestTime = SaveManager.Instance.GetBestTime ();

		SetTimeText (bestTime);
		timeText.color = Color.red;
	}

	void SetTimeText (float t) {
		int min = (int)(t / 60);
		t -= min * 60;

		float second = (int)t;
		t -= second;
		t *= 100;
		t = (int)t;

		string minString = min.ToString ();
		if (min < 10) {
			minString = "0" + min.ToString ();
		}

		string secondString = second.ToString ();
		if (second < 10) {
			secondString = "0" + second.ToString ();
		}

		string tempString = t.ToString ();
		if (t < 10) {
			tempString = "0" + t.ToString ();
		}

		timeText.text = minString + ":" + secondString + ":" + tempString;
	}

	private void CreateTarget (int index) {
		int randomTargetIndex = Random.Range (0, targetPrefabs.Length);
		GameObject target = Instantiate (targetPrefabs [randomTargetIndex], targetPosition [index].position, targetPosition [index].rotation);
		target.GetComponent <ShootGameTarget> ().index = index;
	}

}
