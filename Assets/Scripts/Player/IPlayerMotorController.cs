using UnityEngine;
using System.Collections;

public interface IPlayerMotorController {
	bool IsMoving();
	void MoveToDestination(float speed);
	void MoveToRotation(Quaternion rotation, float rotSpeed);
	void SetDestination(int x, int y);
}
