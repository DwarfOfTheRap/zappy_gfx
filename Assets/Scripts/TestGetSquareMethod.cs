using UnityEngine;
using System.Collections;

public class TestGetSquareMethod : MonoBehaviour {
	
	public ISquare squareToTest;
	public Vector3 sttPosition;

	void Start () {
		squareToTest = GameManagerScript.instance.grid.GetComponent<GridScript> ().controller.GetSquare (2, 2);
		sttPosition = squareToTest.GetPosition ();
	}
}
