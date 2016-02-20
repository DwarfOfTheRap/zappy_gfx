using UnityEngine;
using System.Collections;

public interface IGrid {
	ISquare Instantiate(int index);
	ISquare Instantiate(int index, Vector3 position);
}
