using UnityEngine;
using System.Collections;

public interface IPlayerMotorController {
	bool IsMoving(Vector3 destination);
	void MoveToDestination(Vector3 destination, float speed);
	bool HasHitDestination(Vector3 destination);
	bool HasHitRotation(Quaternion rotation);
	void MoveToRotation(Quaternion rotation, float rotSpeed);
	void Expulsed(Orientation orientation);
	void StopExpulsion();
	Vector3 SetDestination(Vector3 destination);
	void SetRotation (Quaternion rotation);
	void SetPosition (Vector3 vector3);
	
	void EnableHighlight(Color color);
	void DisableHighlight();

	void SetTeamColor(Color color);
}
