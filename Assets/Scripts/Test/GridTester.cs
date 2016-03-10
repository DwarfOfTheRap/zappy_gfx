using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
[RequireComponent(typeof(GridScript))]
public class GridTester : MonoBehaviourTester {
	public MonoBehaviourTest	visionTest;
	public int					vision_test_x;
	public int					vision_test_y;
	public int					vision_test_level;
	public Color				vision_test_color;
	public Orientation			orientation;

	public bool					areInVision;

	protected override void InitTest ()
	{
		if (visionTest.enabled)
			StartCoroutine(WaitForTest (visionTest, visionTestMethod));
	}

	void visionTestMethod ()
	{
		ISquare[] squares = GetComponent<GridScript>().controller.GetVision (vision_test_x, vision_test_y, orientation, vision_test_level);
		foreach (ISquare square in squares)
		{
			square.EnableVision (vision_test_color);
		}
	}
}
#endif