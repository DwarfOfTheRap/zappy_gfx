using System;
using System.Collections.Generic;
using UnityEngine;


	public class TestGridInEditor : MonoBehaviour
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