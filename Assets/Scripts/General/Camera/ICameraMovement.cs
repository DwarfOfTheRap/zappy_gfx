using UnityEngine;
using System.Collections;

public interface ICameraMovement
{
	Vector3 Move (Vector3 direction, float speed);
	Vector3 GoTo (Vector3 position);
	Quaternion Rotate (Quaternion quaternion);
	Vector3 LerpMove (Vector3 destination, float speed);
}