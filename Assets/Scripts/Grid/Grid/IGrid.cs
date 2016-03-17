using UnityEngine;
using System.Collections;

public interface IGrid {
	ISquare Instantiate(int index);
	ISquare Instantiate(int index, Vector3 position);
	float GetInitProgress();
	void Init(int width, int height);
	Vector3 GetTeleporterPosition(Orientation orientation);
}
