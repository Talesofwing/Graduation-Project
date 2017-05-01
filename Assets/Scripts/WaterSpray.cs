using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpray : MonoBehaviour {

	public GameObject spray;
	public AudioClip dropWaterClip;

	void OnTriggerEnter (Collider other) {
		Destroy (Instantiate (spray, other.transform.position, spray.transform.rotation), 5f);
		SoundManager.Instance.PlaySFX (dropWaterClip, 4f);
	}

}
