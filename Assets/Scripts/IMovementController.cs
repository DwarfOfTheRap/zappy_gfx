﻿using UnityEngine;
using System.Collections;

public interface IPlayerMovementController {
	bool IsMoving();
	void MoveToDestination(Vector3 destination, float speed);
	void MoveToRotation(Quaternion rotation, float rotSpeed);
	Vector3 SetDestination(Vector3 destination);
}
