using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerInstantiationController : MonoBehaviour, IPlayerInstantiationController
{
	public GameObject prefab;
	
	void Awake()
	{
		prefab = Resources.Load ("Prefab/PA_Warrior") as GameObject;
	}
	
	public PlayerController Instantiate ()
	{
		GameObject clone = Instantiate (prefab);
		return clone.GetComponent<PlayerScript>().controller;
	}
}

public interface IPlayerInstantiationController
{
	PlayerController Instantiate ();
}
