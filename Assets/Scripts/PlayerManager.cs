using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager {
	public class PlayerNotFoundException : Exception
	{
		public PlayerNotFoundException() {}
		
		public PlayerNotFoundException(string message) : base(message) {}
	}
	public class TwoPlayersWithTheSameIndexException : Exception
	{
		public TwoPlayersWithTheSameIndexException() {}
		
		public TwoPlayersWithTheSameIndexException(string message) : base(message) {}
	}
	private List<PlayerController>			players;
	public GridController					gridController { get; private set; }
	public TeamManager						teamManager { get; private set; }
	public IPlayerInstantiationController	pic { get; private set; }
	
	PlayerManager(){}
	
	public PlayerManager(GridController gridController, TeamManager teamManager, IPlayerInstantiationController pic)
	{
		this.players = new List<PlayerController>();
		this.pic = pic;
		this.gridController = gridController;
	}
	
	public virtual PlayerController GetPlayer(int n)
	{
		try
		{
			return players.Find(x => x.index == n);
		}
		catch (Exception)
		{
			throw new PlayerNotFoundException();
		}
	}
	
	public PlayerController[] GetPlayersInTeam(Team team)
	{
		List<PlayerController> tmp = new List<PlayerController>();
		
		foreach (PlayerController player in players)
		{
			if (player.team == team)
				tmp.Add (player);
		}
		if (tmp.Count == 0)
			return null;
		return tmp.ToArray ();
	}
}
