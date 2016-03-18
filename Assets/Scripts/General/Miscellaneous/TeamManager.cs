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
	public class DuplicateTeamException : TeamManagerException
	{
		public DuplicateTeamException() : base() {}
		public DuplicateTeamException(string message) : base(message) {}
	}
	public delegate void TeamMethod(Team team);
	public static event TeamMethod OnTeamAdded;
	public	List<Team>		teams;
	private Stack<Color>	colors;

	public Dictionary<Color, Color[]> triad;

	public TeamManager()
	{
		this.colors = new Stack<Color>();
		this.teams = new List<Team>();
		this.triad = new Dictionary<Color, Color[]>();

		this.colors.Push (new Vector4(1, 0.5f, 1, 1));
		this.colors.Push (new Vector4(0.5f, 1, 0.5f, 1));
		this.colors.Push (new Vector4(1, 0.5f, 0, 1));
		this.colors.Push (Color.white);
		this.colors.Push (Color.blue);
		this.colors.Push (Color.red);
		this.colors.Push (Color.green);
		this.colors.Push (Color.yellow);
		this.colors.Push (Color.magenta);
		this.colors.Push (Color.cyan);

		this.triad.Add (Color.cyan, new Color[]{Color.magenta, Color.yellow});
		this.triad.Add (Color.magenta, new Color[]{Color.yellow, Color.cyan});
		this.triad.Add (Color.yellow, new Color[]{Color.cyan, Color.magenta});
		this.triad.Add (Color.green, new Color[]{Color.blue, Color.red});
		this.triad.Add (Color.blue, new Color[]{Color.red, Color.green});
		this.triad.Add (Color.red, new Color[]{Color.green, Color.blue});
		this.triad.Add (Color.white, new Color[]{Color.white, Color.white});
	}

	public virtual Team			FindTeam(string name)
	{
		if (teams.Count == 0)
			throw new NoTeamException();
		Team res = teams.Find (x => x.name == name);
		if (res == null)
			throw new TeamNotFoundException();
		return res;
	}

	public virtual Team			CreateTeam(string name)
	{
		if (teams.Find (x => x.name == name) != null)
			throw new DuplicateTeamException();
		Team team = new Team(name, GetNewTeamColor ());
		this.teams.Add (team);
		if (OnTeamAdded != null)
			OnTeamAdded(team);
		return team;
	}

	private Color				GetNewTeamColor()
	{
		if (this.colors.Count > 0)
			return this.colors.Pop ();
		return new Color(UnityEngine.Random.Range (0.0f, 1.0f), UnityEngine.Random.Range (0.0f, 1.0f), UnityEngine.Random.Range (0.0f, 1.0f));
	}
}