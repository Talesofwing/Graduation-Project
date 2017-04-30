using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionModel : MonoBehaviour {

	public Color normal = Color.white;			// 普通狀態的顏色
	public Color highlight = Color.red;			// 選中狀態的顏色
	public Color pressed = Color.green;			// 點擊狀態的顏色

	// 點擊事件
	public event HandEventHandle triggerDownEvents;		// 按下
	public event HandEventHandle triggerUpEvents;		// 彈起

	// 選中事件
	public event HandEventHandle highlightEvents;		
	public event HandEventHandle unHighlightEvents;

	private MeshRenderer meshRenderer;

	void Start () {
		triggerDownEvents += HandleTriggerDown;
		triggerUpEvents += HandleTriggerUp;
		highlightEvents += HandleHighlight;
		unHighlightEvents += HandleUnHighlight;
		meshRenderer = GetComponent <MeshRenderer> ();
	}

	void OnTriggerEnter (Collider other) {

		if (other.CompareTag ("Hand")) {
			HandController handController = other.GetComponent <HandController> ();

			if (handController != null) {
				handController.triggerDownEvents += triggerDownEvents;
				handController.triggerUpEvents += triggerUpEvents;
			}

			if (highlightEvents != null)
				highlightEvents (handController.Hand);

		}

	}

	void OnTriggerExit (Collider other) {

		if (other.CompareTag ("Hand")) {
			HandController handController = other.GetComponent <HandController> ();

			if (handController != null) {
				handController.triggerDownEvents -= triggerDownEvents;
				handController.triggerUpEvents -= triggerUpEvents;
			}

			if (unHighlightEvents != null)
				unHighlightEvents (handController.Hand);

		}

	}

	private void HandleTriggerDown (SteamVR_TrackedObject hand) {
		meshRenderer.material.color = pressed;
	}

	private void HandleTriggerUp (SteamVR_TrackedObject hand) {
		meshRenderer.material.color = highlight;
	}
		
	bool isShock = true;
	private void HandleHighlight (SteamVR_TrackedObject hand) {
		isShock = true;
		meshRenderer.material.color = highlight;
		StartShock ((int)hand.index);
	}

	void StartShock (int index) {
		StartCoroutine (Shock (index));
	}

	private float duration = 0.05f;
	IEnumerator Shock (int index) {
		Invoke ("StopShock", duration);

		while (isShock) {
			SteamVR_Controller.Input (index).TriggerHapticPulse ();
			yield return null;
		}
			
	}

	void StopShock () {
		isShock = false;
	}

	private void HandleUnHighlight (SteamVR_TrackedObject hand) {
		meshRenderer.material.color = normal;
	}

}
