using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;

[TestFixture]
public class OrientationManagerTest {
	[Test]
	public void GetDestinationOrientation_Test_Orientation()
	{
		// Arrange
		Orientation expected_result0 = Orientation.NONE;
		Orientation expected_result1 = Orientation.NORTH;
		Orientation expected_result2 = Orientation.EAST;
		Orientation expected_result3 = Orientation.SOUTH;
		Orientation expected_result4 = Orientation.WEST;
		// Act
		Orientation result0 = OrientationManager.GetDestinationOrientation (Vector3.zero, Vector3.zero);
		Orientation result1 = OrientationManager.GetDestinationOrientation (Vector3.zero, new Vector3(0, 0, 1));
		Orientation result2 = OrientationManager.GetDestinationOrientation (Vector3.zero, new Vector3(1, 0, 0));
		Orientation result3 = OrientationManager.GetDestinationOrientation (Vector3.zero, new Vector3(0, 0, -1));
		Orientation result4 = OrientationManager.GetDestinationOrientation (Vector3.zero, new Vector3(-1, 0, 0));
		
		// Assert
		Assert.AreEqual (expected_result0, result0);
		Assert.AreEqual (expected_result1, result1);
		Assert.AreEqual (expected_result2, result2);
		Assert.AreEqual (expected_result3, result3);
		Assert.AreEqual (expected_result4, result4);
	}

	[Test]
	public void GetDirectionVector_Test_Orientation()
	{
		// Arrange
		Vector2 expected_result0 = Vector2.up;
		Vector2 expected_result1 = Vector2.right;
		Vector2 expected_result2 = Vector2.down;
		Vector2 expected_result3 = Vector2.left;
		Vector2 expected_result4 = Vector2.zero;
		// Act
		Vector2 result0 = OrientationManager.GetDirectionVector(Orientation.NORTH);
		Vector2 result1 = OrientationManager.GetDirectionVector(Orientation.EAST);
		Vector2 result2 = OrientationManager.GetDirectionVector(Orientation.SOUTH);
		Vector2 result3 = OrientationManager.GetDirectionVector(Orientation.WEST);
		Vector2 result4 = OrientationManager.GetDirectionVector(Orientation.NONE);
		// Assert
		Assert.AreEqual (expected_result0, result0);
		Assert.AreEqual (expected_result1, result1);
		Assert.AreEqual (expected_result2, result2);
		Assert.AreEqual (expected_result3, result3);
		Assert.AreEqual (expected_result4, result4);
	}

	[Test]
	public void GetRotation_Test_Orientation()
	{
		// Arrange
		Quaternion expected_result0 = Quaternion.Euler(0, 0, 0);
		Quaternion expected_result1 = Quaternion.Euler(0, 90, 0);
		Quaternion expected_result2 = Quaternion.Euler(0, 180, 0);
		Quaternion expected_result3 = Quaternion.Euler (0, 270, 0);
		Quaternion expected_result4 = Quaternion.identity;
		// Act
		Quaternion result0 = OrientationManager.GetRotation(Orientation.NORTH);
		Quaternion result1 = OrientationManager.GetRotation(Orientation.EAST);
		Quaternion result2 = OrientationManager.GetRotation(Orientation.SOUTH);
		Quaternion result3 = OrientationManager.GetRotation(Orientation.WEST);
		Quaternion result4 = OrientationManager.GetRotation(Orientation.NONE);
		// Assert
		Assert.AreEqual (expected_result0, result0);
		Assert.AreEqual (expected_result1, result1);
		Assert.AreEqual (expected_result2, result2);
		Assert.AreEqual (expected_result3, result3);
		Assert.AreEqual (expected_result4, result4);
	}
	
	[Test]
	public void GetAnimationOrientation_Test_Orientation()
	{
		// Arrange
		Orientation expected_result0 = Orientation.NORTH;
		Orientation expected_result1 = Orientation.WEST;
		Orientation expected_result2 = Orientation.SOUTH;
		Orientation expected_result3 = Orientation.EAST;
		Orientation expected_result4 = Orientation.EAST;
		Orientation expected_result5 = Orientation.NORTH;
		Orientation expected_result6 = Orientation.WEST;
		Orientation expected_result7 = Orientation.SOUTH;
		Orientation expected_result8 = Orientation.SOUTH;
		Orientation expected_result9 = Orientation.EAST;
		Orientation expected_result10 = Orientation.NORTH;
		Orientation expected_result11 = Orientation.WEST;
		Orientation expected_result12 = Orientation.WEST;
		Orientation expected_result13 = Orientation.SOUTH;
		Orientation expected_result14 = Orientation.EAST;
		Orientation expected_result15 = Orientation.NORTH;
		// Act
		Orientation result0 = OrientationManager.GetAnimationOrientation(Orientation.NORTH, Orientation.NORTH);
		Orientation result1 = OrientationManager.GetAnimationOrientation(Orientation.NORTH, Orientation.EAST);
		Orientation result2 = OrientationManager.GetAnimationOrientation(Orientation.NORTH, Orientation.SOUTH);
		Orientation result3 = OrientationManager.GetAnimationOrientation(Orientation.NORTH, Orientation.WEST);
		Orientation result4 = OrientationManager.GetAnimationOrientation(Orientation.EAST, Orientation.NORTH);
		Orientation result5 = OrientationManager.GetAnimationOrientation(Orientation.EAST, Orientation.EAST);
		Orientation result6 = OrientationManager.GetAnimationOrientation(Orientation.EAST, Orientation.SOUTH);
		Orientation result7 = OrientationManager.GetAnimationOrientation(Orientation.EAST, Orientation.WEST);
		Orientation result8 = OrientationManager.GetAnimationOrientation(Orientation.SOUTH, Orientation.NORTH);
		Orientation result9 = OrientationManager.GetAnimationOrientation(Orientation.SOUTH, Orientation.EAST);
		Orientation result10 = OrientationManager.GetAnimationOrientation(Orientation.SOUTH, Orientation.SOUTH);
		Orientation result11 = OrientationManager.GetAnimationOrientation(Orientation.SOUTH, Orientation.WEST);
		Orientation result12 = OrientationManager.GetAnimationOrientation(Orientation.WEST, Orientation.NORTH);
		Orientation result13 = OrientationManager.GetAnimationOrientation(Orientation.WEST, Orientation.EAST);
		Orientation result14 = OrientationManager.GetAnimationOrientation(Orientation.WEST, Orientation.SOUTH);
		Orientation result15 = OrientationManager.GetAnimationOrientation(Orientation.WEST, Orientation.WEST);
		// Assert
		Assert.AreEqual (expected_result0, result0);
		Assert.AreEqual (expected_result1, result1);
		Assert.AreEqual (expected_result2, result2);
		Assert.AreEqual (expected_result3, result3);
		Assert.AreEqual (expected_result4, result4);
		Assert.AreEqual (expected_result5, result5);
		Assert.AreEqual (expected_result6, result6);
		Assert.AreEqual (expected_result7, result7);
		Assert.AreEqual (expected_result8, result8);
		Assert.AreEqual (expected_result9, result9);
		Assert.AreEqual (expected_result10, result10);
		Assert.AreEqual (expected_result11, result11);
		Assert.AreEqual (expected_result12, result12);
		Assert.AreEqual (expected_result13, result13);
		Assert.AreEqual (expected_result14, result14);
		Assert.AreEqual (expected_result15, result15);
	}

}
