using UnityEngine;
using System.Collections;

public interface ISquareInstantiationController {
	ISquare Instantiate(int index);
	ISquare Instantiate(int index, Vector3 position);
}
