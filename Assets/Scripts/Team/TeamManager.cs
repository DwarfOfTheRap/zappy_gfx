using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TeamManager
{
	public abstract class TeamManagerException : Exception
	{
		public TeamManagerException() : base() {}
		public TeamManagerException(string message) : base(message) {}
	}
	public class TeamNotFoundException : TeamManagerException
	{
		public TeamNotFoundException() : base() {}
		public TeamNotFoundException(string message) : base(message) {}
	}
	public class NoTeamException : TeamManagerException
	{
		public NoTeamException() : base() {}
		public NoTeamException(string message) : base(message) {}
	}
	public delegate void TeamMethod(Team team);
	public static event TeamMethod OnTeamAdded;
	public	List<Team>		teams;
	private Stack<Color>	colors;

	public TeamManager()
	{
		this.colors = new Stack<Color>();
		this.teams = new List<Team>();

		this.colors.Push (new Vector4(1, 0.5f, 1, 1));
		this.colors.Push (new Vector4(0.5f, 1, 0.5f, 1));
		this.colors.Push (new Vector4(1, 0.5f, 0, 1));
		this.colors.Push (Color.white);
		this.colors.Push (Color.red);
		this.colors.Push (Color.blue);
		this.colors.Push (Color.green);
		this.colors.Push (Color.yellow);
		this.colors.Push (Color.magenta);
		this.colors.Push (Color.cyan);
	}

	public Team			findTeam(string name)
	{
		if (teams.Count == 0)
			throw new NoTeamException();
		Team res = teams.Find (x => x.name == name);
		if (res == null)
			throw new TeamNotFoundException();
		return res;
	}

	public Team			createTeam(string name)
	{
		Team team = new Team(name, _getNewTeamColor ());
		this.teams.Add (team);
		if (OnTeamAdded != null)
			OnTeamAdded(team);
		return team;
	}

	private Color		_getNewTeamColor()
	{
		if (this.colors.Count > 0)
			return this.colors.Pop ();
		return new Color(UnityEngine.Random.Range (0.0f, 1.0f), UnityEngine.Random.Range (0.0f, 1.0f), UnityEngine.Random.Range (0.0f, 1.0f));
	}
}