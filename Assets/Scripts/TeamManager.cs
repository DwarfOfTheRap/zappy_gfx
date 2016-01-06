using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TeamManager
{
	class TeamNotFoundException : Exception
	{
		public TeamNotFoundException() : base() {}
		public TeamNotFoundException(string message) : base(message) {}
	}

	public List<Team>	teams;

	public Team			FindTeam(string name)
	{
		Team res = teams.Find (x => x.name == name);
		if (res == null)
			throw new TeamNotFoundException();
		return res;
	}
}

