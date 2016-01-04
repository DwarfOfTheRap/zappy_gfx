using UnityEngine;
using System.Collections;

public interface IGrid {
	ISquare Instantiate(int index);
	ISquare Instantiate(int index, Vector3 position);
	void	InitTeleporters(float sizex, float sizey, float sizez, int width, int height);
	Vector3 GetTeleporterPosition(Orientation orientation);
}
