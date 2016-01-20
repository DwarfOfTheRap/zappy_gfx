using UnityEngine;
using System.Collections;

public class TestSquareInfo : MonoBehaviour {

	public SquareContentUI sIS;

	void FixedUpdate ()
	{
		sIS.square = sIS.OnLeftMouseClick (Camera.main.WorldToScreenPoint(this.gameObject.transform.localPosition));
	}
}
