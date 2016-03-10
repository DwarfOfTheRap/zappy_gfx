using UnityEngine;
using System.Collections;

public interface IPlayerMotorController {
	Vector3 SetDestination(Vector3 destination);
	void SetRotation (Quaternion rotation);
	void SetPosition (Vector3 vector3);

	bool IsMoving(Vector3 destination);
	void MoveToDestination(Vector3 destination, float speed);
	bool HasReachedDestination(Vector3 destination);

	bool HasReachedRotation(Quaternion rotation);
	void MoveToRotation(Quaternion rotation, float rotSpeed);

	void Broadcast(string message);

	void Expulsed(Orientation orientation);
	void StopExpulsion();

	void EnableHighlight(Color color);
	void DisableHighlight();

	void Destroy ();

	void SetTeamColor(Color color);
}
