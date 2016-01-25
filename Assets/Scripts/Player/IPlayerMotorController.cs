using UnityEngine;
using System.Collections;

public interface IPlayerMotorController {
	bool IsMoving(Vector3 destination);
	void MoveToDestination(Vector3 destination, float speed);
	void MoveToRotation(Quaternion rotation, float rotSpeed);
	void Expulsed(Orientation orientation);
	void StopExpulsion();
	Vector3 SetDestination(Vector3 destination);
	void SetPosition (Vector3 vector3);

	void SetTeamColor(Color color);
}
