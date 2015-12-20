using UnityEngine;
using System;
using System.Collections;

public class SquareScript : MonoBehaviour, ISquare
{
	public Vector3 GetPosition ()
	{
		return transform.position;
	}

	public float GetBoundX ()
	{
		return GetComponent<Renderer>().bounds.size.x;
	}

	public float GetBoundY ()
	{
		return GetComponent<Renderer>().bounds.size.y;
	}

	public float GetBoundZ ()
	{
		return GetComponent<Renderer>().bounds.size.z;
	}

	public void Destroy ()
	{
		Destroy (this);
	}
}

public interface ISquare
{
	Vector3 GetPosition();
	float GetBoundX();
	float GetBoundY();
	float GetBoundZ();
	void Destroy();
}

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
