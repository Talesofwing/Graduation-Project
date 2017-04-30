using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void DragEventHandle (bool isOn);

public class DragButton : MonoBehaviour {

	public Text onText;
	public Text offText;
	public Color highlightColor;
	public Color unHighlightColor;
	public event DragEventHandle dragUpEvents;

	private InteractionModel s;				// 交換模型

	private bool isTriggerDown = false;		// 是否按下Trigger

	private SteamVR_TrackedObject hand;

	private Border border;					// 邊界

	private bool isOn = true;

	void Awake () {
		border = BorderHelper.GetBackMenuBoardButtonBorder ();
	}

	void Start () {
		s = GetComponent <InteractionModel> ();
		s.triggerUpEvents += HandleTriggerUp;
		s.triggerDownEvents += HandleTriggerDown;
		s.highlightEvents += HandleHighlight;
		s.unHighlightEvents += HandleUnHighlight;
	}

	void Update () {

		if (isTriggerDown && hand) {
			float x = hand.transform.position.x;
			Vector3 pos = transform.position;
			x = Mathf.Max (border.minBorder.x, x);
			x = Mathf.Min (border.maxBorder.x, x);
			pos.x = x;
			transform.position = pos;
		}

	}

	private void HandleTriggerUp (SteamVR_TrackedObject hand) {

		if (! isTriggerDown)
			return;
		
		isTriggerDown = false;
		this.hand = null;
		UpdateButtonPos ();

		if (dragUpEvents != null)
			dragUpEvents (isOn);

	}

	private void HandleTriggerDown (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayPressedSound ();
		this.hand = hand;
		isTriggerDown = true;
	}

	private void HandleHighlight (SteamVR_TrackedObject hand) {
		SoundManager.Instance.PlayHighlightSound ();
	}

	private void HandleUnHighlight (SteamVR_TrackedObject hand) {
		HandleTriggerUp (hand);
	}

	// 更新按鈕位置
	void UpdateButtonPos () {
		float mid = (border.maxBorder.x + border.minBorder.x) / 2f;
		float transformX = transform.position.x;

		float x;
		if (transformX > mid) {
			// on
			onText.color = highlightColor;
			offText.color = unHighlightColor;
			x = border.maxBorder.x;
			isOn = true;
		} else {
			// off
			offText.color = highlightColor;
			onText.color = unHighlightColor;
			x = border.minBorder.x;
			isOn = false;
		}
				
		Vector3 pos = transform.position;
		pos.x = x;
		transform.position = pos;
	}

	public void SetOff () {
		Vector3 targetPos = border.minBorder;
		targetPos.y = transform.position.y;
		transform.position = targetPos;
		offText.color = highlightColor;
		onText.color = unHighlightColor;
		isOn = false;

		if (dragUpEvents != null)
			dragUpEvents (isOn);
	}

	public void SetOn () {
		Vector3 targetPos = border.maxBorder;
		targetPos.y = transform.position.y;
		transform.position = targetPos;
		onText.color = highlightColor;
		offText.color = unHighlightColor;
		isOn = true;

		if (dragUpEvents != null)
			dragUpEvents (isOn);
	}

}
