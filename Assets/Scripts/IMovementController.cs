using UnityEngine;
using System.Collections;

public interface IPlayerMovementController {
	bool IsMoving(Vector3 destination);
	void MoveToDestination(Vector3 destination, float speed);
	void MoveToRotation(Quaternion rotation, float rotSpeed);
	void Expulsed(Orientation orientation);
	void StopExpulsion();
	Vector3 SetDestination(Vector3 destination);
}
