using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBGMButton : MonoBehaviour {

	private DragButton dragButton;

	void Start () {
		dragButton = GetComponent <DragButton> ();
		dragButton.dragUpEvents += HandleDragUpEvent;
	}

	private void HandleDragUpEvent (bool isOn) {
		SoundManager.Instance.SetBGMVolume (isOn);
	}

}
