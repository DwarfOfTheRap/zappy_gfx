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
		this.eggs = new List<EggController>();
		this.pic = pic;
		this.eic = eic;
		this.teamManager = teamManager;
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

	public virtual PlayerController SetPlayerConnection(int n, int x, int y, Orientation o, int l, string name)
	{
		PlayerController controller = pic.InstantiatePlayer();
		if (players.Find (pl => pl.index == n) != null)
		{
			controller.Destroy ();
			throw new TwoPlayersWithTheSameIndexException();
		}
		try
		{
			controller.Init (x, y, o, l, n, teamManager.findTeam(name), gridController);
		}
		catch (Exception e)
		{
			controller.Destroy ();
			throw e;
		}
		players.Add (controller);
		return controller;
	}

	public virtual PlayerController SetPlayerPosition(int n, int x, int y, Orientation o)
	{
		PlayerController player = GetPlayer (n);
		player.SetPosition (x, y, gridController);
		player.SetPlayerOrientation (o);
		return player;
	}

	public virtual PlayerController SetPlayerLevel(int n, int l)
	{
		PlayerController player = GetPlayer (n);
		player.level = l;
		return player;
	}

	public virtual PlayerController SetPlayerInventory(int n, int nourriture, int linemate, int deraumere, int sibur, int mendiane, int phiras, int thystame)
	{
		PlayerController player = GetPlayer (n);
		player.inventory.nourriture = nourriture;
		player.inventory.linemate = linemate;
		player.inventory.deraumere = deraumere;
		player.inventory.sibur = sibur;
		player.inventory.mendiane = mendiane;
		player.inventory.phiras = phiras;
		player.inventory.thystame = thystame;
		return player;
	}

	public virtual PlayerController SetPlayerExpulse(int n)
	{
		PlayerController player = GetPlayer (n);
		player.Expulse ();
		return player;
	}

	public virtual PlayerController SetPlayerBroadcast(int n, string message)
	{
		PlayerController player = GetPlayer (n);
		player.Broadcast(message);
		return player;
	}

	public virtual PlayerController SetPlayerIncantatePrimary(int n)
	{
		PlayerController player = GetPlayer (n);
		player.IncantatePrimary ();
		return player;
	}

	public virtual PlayerController SetPlayerIncantateSecondary(int n)
	{
		PlayerController player = GetPlayer (n);
		player.IncantateSecondary ();
		return player;
	}

	public virtual List<PlayerController> SetPlayersStopIncantate(int x, int y, int incantationResult)
	{
		var players = GameManagerScript.instance.grid.controller.GetSquare (x, y).GetResources ().players;
		foreach (var player in GameManagerScript.instance.grid.controller.GetSquare (x, y).GetResources ().players)
		{
			player.StopIncantating ();
		}
		return players;
	}

	public virtual PlayerController SetPlayerLayEgg(int n)
	{
		PlayerController player = GetPlayer (n);
		player.LayEgg ();
		return player;
	}

	public virtual PlayerController SetPlayerThrowResource(int n, int i)
	{
		PlayerController player = GetPlayer (n);
		player.ThrowItem ();
		return player;
	}

	public virtual PlayerController SetPlayerTakeResource(int n, int i)
	{
		PlayerController player = GetPlayer (n);
		player.GrabItem ();
		return player;
	}

	public virtual PlayerController SetPlayerDeath(int n)
	{
		PlayerController player = GetPlayer (n);
		player.Die ();
		players.Remove (player);
		return player;
	}

	public virtual EggController SetEggCreation(int e, int n, int x, int y)
	{
		EggController egg = this.eic.InstantiateEgg();
		PlayerController player = GetPlayer (n);
		player.StopLayingEgg ();
		eggs.Add (egg.Init (x, y, e, player, gridController));
		return egg;
	}

	public virtual EggController SetEggHatch(int e)
	{
		EggController egg = GetEgg (e);
		egg.Hatch();
		return egg;
	}

	public virtual EggController SetPlayerToEggConnection(int e)
	{
		EggController egg = GetEgg (e);
		egg.PlayerConnection();
		return egg;
	}

	public virtual EggController SetEggDie(int e)
	{
		EggController egg = GetEgg (e);
		egg.Die();
		eggs.Remove (egg);
		return egg;
	}
}
