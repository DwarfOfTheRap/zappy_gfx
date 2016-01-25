using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
public class TestSquareInfo : MonoBehaviour {

	public SquareInfoScript sIS;

	void FixedUpdate ()
	{
		sIS.square = sIS.OnLeftMouseClick (Camera.main.WorldToScreenPoint(this.gameObject.transform.localPosition));
	}
}
#endif
