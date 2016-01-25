using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class TestNumberOfEachTile : MonoBehaviour
{
	public int numberOfTiles1;
	public int numberOfTiles2;

	public void Start ()
	{
		GameObject[] allTiles1 = GameObject.FindGameObjectsWithTag ("Floor1");
		GameObject[] allTiles2 = GameObject.FindGameObjectsWithTag ("Floor2");
		numberOfTiles1 = allTiles1.Length;
		numberOfTiles2 = allTiles2.Length;
	}
}
#endif