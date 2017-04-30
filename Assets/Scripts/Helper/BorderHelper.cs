using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border {
	public Vector3 minBorder = Vector3.zero;
	public Vector3 maxBorder = Vector3.zero;

	public Border (Vector3 minBorder, Vector3 maxBorder) {
		this.minBorder = minBorder;
		this.maxBorder = maxBorder;
	}

	public override string ToString () {
		return string.Format ("[Min Border:" + minBorder + "]" + ", [Max Border:" + maxBorder + "]");
	}

}

public class BorderHelper : MonoBehaviour {

	public static Border GetBackMenuBoardButtonBorder () {
		Border border = new Border (new Vector3 (-0.0834f, 1.3211f, -1.178f), new Vector3 (0.0578f, 1.3211f, -1.178f));
		return border;
	}

	public static Border GetBalloonBorder () {
		Border border = new Border (new Vector3 (-6.5f, 9f, -6.5f), new Vector3 (6.5f, 13f, -1f));
		return border;
	}
}
