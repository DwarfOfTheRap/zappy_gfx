using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Team
{
	public string	name { get; private set; }
	public Color	color { get; private set; }
	
	public Team(string name, Color color)
	{
		this.name = name;
		this.color = color;
	}
}