using UnityEngine;
using System.Collections;

public interface IInputManager
{
	bool MoveLeft();
	bool MoveRight();
	float HorizontalMovementValue();

	bool MoveForward();
	bool MoveBackward();
	float VerticalMovementValue();

	bool MoveUp();
	bool MoveDown();

	bool ResetCamera();

	bool ScrollUp();
	bool ScrollDown();
	float DeltaScroll();
	
	bool DoubleMoveSpeed();
	bool StandardMoveSpeed();

	bool LeftClick();
	bool DoubleLeftClick();
	bool RightClick();

	bool MousingOverGameObject();
}