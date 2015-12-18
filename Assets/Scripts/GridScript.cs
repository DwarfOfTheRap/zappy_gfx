using UnityEngine;
using System;
using System.Collections;

public class GridScript : MonoBehaviour, ISquareInstantiationController {

	public SquareScript[] prefabs = new SquareScript[2];
	public GridController controller;

	void OnEnable()
	{
		controller.SetSquareInstantiationController (this);
		controller.Start ();
	}

	#region ISquareInstantiationController implementation

	public ISquare Instantiate (int index)
	{
		return GameObject.Instantiate (prefabs[index]) as SquareScript;
	}

	public ISquare Instantiate (int index, Vector3 position)
	{
		SquareScript clone = GameObject.Instantiate (prefabs[index]) as SquareScript;
		clone.transform.SetParent (this.transform);
		clone.transform.localPosition = position;
		return clone;
	}

	#endregion
}
