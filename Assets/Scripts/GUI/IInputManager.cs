using UnityEngine;
using System.Collections;

public interface IInputManager {
	bool MoveLeft();
	float LeftMovementValue();
	bool MoveRight();
	float RightMovementValue();
	bool MoveForward();
	float ForwardMovementValue();
	bool MoveBackward();
	float BackwardMovementValue();

	bool MoveUp();
	bool MoveDown();

	bool ResetCamera();

	bool ScrollUp();
	bool ScrollDown();
	float DeltaScroll();
	
	bool DoubleMoveSpeed();

	bool LeftClick();
	bool DoubleLeftClick();
	bool RightClick();

	bool MousingOverGameObject();
}