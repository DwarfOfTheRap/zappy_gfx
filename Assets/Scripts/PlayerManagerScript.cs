using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerManagerScript {
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

	PlayerManagerScript(){}

	public PlayerManagerScript(GridController gridController, TeamManager teamManager, IPlayerInstantiationController pic)
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

	public PlayerController SetPlayerConnection(int n, int x, int y, Orientation o, int l, string name)
	{
		PlayerController controller = pic.Instantiate();
		if (players.Find (pl => pl.index == n) != null)
			throw new TwoPlayersWithTheSameIndexException();
		controller.Init (x, y, o, l, n, teamManager.FindTeam(name), gridController);
		players.Add (controller);
		return controller;
	}

	public PlayerController SetPlayerPosition(int n, int x, int y, Orientation o)
	{
		PlayerController player = GetPlayer (n);
		player.SetPosition (x, y, gridController);
		player.SetPlayerOrientation (o);
		return player;
	}

	public PlayerController SetPlayerLevel(int n, int l)
	{
		PlayerController player = GetPlayer (n);
		player.level = l;
		return player;
	}

	public PlayerController SetPlayerInventory(int n, int[] inventory)
	{
		throw new NotImplementedException();
	}

	public PlayerController SetPlayerExpulse(int n)
	{
		PlayerController player = GetPlayer (n);
		player.Expulse ();
		return player;
	}

	public PlayerController SetPlayerBroadcast(int n)
	{
		throw new NotImplementedException();
	}

	public PlayerController SetPlayerIncantate(int n)
	{
		PlayerController player = GetPlayer (n);
		player.Incantate ();
		return player;
	}

	public PlayerController SetPlayerStopIncantate(int n)
	{
		PlayerController player = GetPlayer (n);
		player.StopIncantating ();
		return player;
	}

	public PlayerController SetPlayerLayEgg(int n)
	{
		throw new NotImplementedException();
	}

	public PlayerController SetPlayerThrowResource(int n, int i)
	{
		throw new NotImplementedException();
	}

	public PlayerController SetPlayerTakeResource(int n, int i)
	{
		throw new NotImplementedException();
	}

	public PlayerController SetPlayerDeath(int n)
	{
		PlayerController player = GetPlayer (n);
		player.Die ();
		return player;
	}

	public PlayerController SetEggCreation(int e, int n, int x, int y)
	{
		throw new NotImplementedException();
	}

	public PlayerController SetEggHatch(int e)
	{
		throw new NotImplementedException();
	}

	public PlayerController SetEggDie(int e)
	{
		throw new NotImplementedException();
	}
}
