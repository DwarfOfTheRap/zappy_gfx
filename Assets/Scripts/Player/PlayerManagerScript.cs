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
	public class EggNotFoundException : Exception
	{
		public EggNotFoundException() {}
		public EggNotFoundException(string message) : base(message) {}
	}
	public class TwoPlayersWithTheSameIndexException : Exception
	{
		public TwoPlayersWithTheSameIndexException() {}
		
		public TwoPlayersWithTheSameIndexException(string message) : base(message) {}
	}
	public List<PlayerController>			players;
	public List<EggController>				eggs;
	public GridController					gridController { get; private set; }
	public TeamManager						teamManager { get; private set; }
	public IPlayerInstantiationController	pic { get; private set; }
	public IEggInstantiationController		eic { get; private set; }

	PlayerManagerScript(){}

	public PlayerManagerScript(GridController gridController, TeamManager teamManager, IPlayerInstantiationController pic, IEggInstantiationController eic)
	{
		this.players = new List<PlayerController>();
		this.pic = pic;
		this.eic = eic;
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

	public virtual EggController GetEgg(int e)
	{
		try
		{
			return eggs.Find (x => x.index == e);
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
			if (player.team == team && !player.dead)
				tmp.Add (player);
		}
		if (tmp.Count == 0)
			return null;
		return tmp.ToArray ();
	}

	public PlayerController SetPlayerConnection(int n, int x, int y, Orientation o, int l, string name)
	{
		PlayerController controller = pic.InstantiatePlayer();
		if (players.Find (pl => pl.index == n) != null)
			throw new TwoPlayersWithTheSameIndexException();
		controller.Init (x, y, o, l, n, teamManager.findTeam(name), gridController);
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

	public PlayerController SetPlayerIncantatePrimary(int n)
	{
		PlayerController player = GetPlayer (n);
		player.IncantatePrimary ();
		return player;
	}

	public PlayerController SetPlayerIncantateSecondary(int n)
	{
		PlayerController player = GetPlayer (n);
		player.IncantateSecondary ();
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
		PlayerController player = GetPlayer (n);
		player.ThrowItem ();
		return player;
	}

	public PlayerController SetPlayerTakeResource(int n, int i)
	{
		PlayerController player = GetPlayer (n);
		player.GrabItem ();
		return player;
	}

	public PlayerController SetPlayerDeath(int n)
	{
		PlayerController player = GetPlayer (n);
		player.Die ();
		return player;
	}

	public EggController SetEggCreation(int e, int n, int x, int y)
	{
		EggController egg = this.eic.InstantiateEgg();
		PlayerController player = GetPlayer (n);
		eggs.Add (egg.Init (x, y, e, player, gridController));
		return egg;
	}

	public EggController SetEggHatch(int e)
	{
		EggController egg = GetEgg (e);
		egg.Hatch();
		return egg;
	}

	public EggController SetPlayerToEggConnection(int e)
	{
		EggController egg = GetEgg (e);
		egg.PlayerConnection();
		return egg;
	}

	public EggController SetEggDie(int e)
	{
		EggController egg = GetEgg (e);
		egg.Die();
		return egg;
	}
}
