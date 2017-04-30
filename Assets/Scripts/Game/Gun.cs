using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public AudioClip fireClip;

	public Transform shootPos;
	public GameObject shootParticle;
	public GameObject bulletPrefab;

	public float cooldown = 0.25f;
	private float time = 0f;

	private bool canFire = true;

	public MeshRenderer[] body;
	public MeshRenderer[] click;

	public SteamVR_TrackedObject hand;

	private Vector3 pos;
	void Start () {
		pos = transform.localPosition;
	}

	void Update () {
		
		if (!canFire) {
			time += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (transform.localPosition, pos, Time.deltaTime);

			if (time >= cooldown) {
				transform.localPosition = pos;
				time = 0;
				canFire = true;
			}

		}

	}

	void OnEnable () {
		Color bodyColor = ColorHelper.GetRandomColor ();
		for (int i = 0; i < body.Length; i++) {
			body[i].material.color = bodyColor;
			body[i].material.SetColor ("_EmissionColor", bodyColor);
		}

		Color clickColor = ColorHelper.GetRandomColor ();
		for (int i = 0; i < click.Length; i++) {
			click [i].material.color = clickColor;
			click [i].material.SetColor ("_EmissionColor", clickColor);
		}
	}

	bool isShock = true;
	public void Fire () {
		if (!canFire)
			return;

		float randomForce = Random.Range (0.05f, 0.08f);
		Vector3 backPos = -transform.forward * randomForce;
		transform.position += backPos;

		SoundManager.Instance.PlaySFX (fireClip);

		StartShock ();

		canFire = false;

		GameObject particle = Instantiate (shootParticle, shootPos.position, shootPos.rotation) as GameObject;
		particle.transform.SetParent (shootPos);
		particle.GetComponent <ParticleSystem> ().Play ();
		Destroy (particle, 2f);

		GameObject bullet = Instantiate (bulletPrefab, shootPos.position, shootPos.rotation) as GameObject;
		bullet.GetComponent <Rigidbody> ().AddForce (shootPos.forward * 3000f);
		Destroy (bullet, 20f);
	}

	void StartShock () {
		isShock = true;
		StartCoroutine (Shock ());
	}

	void StopShock () {
		isShock = false;
	}

	private float duration = 0.05f;
	IEnumerator Shock () {
		Invoke ("StopShock", duration);

		while (isShock) {
			SteamVR_Controller.Input ((int)hand.index).TriggerHapticPulse (1000);
			yield return null;
		}

	}

}
