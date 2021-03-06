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
	public class TwoEggsWithTheSameIndexException : Exception
	{
		public TwoEggsWithTheSameIndexException() {}
		
		public TwoEggsWithTheSameIndexException(string message) : base(message) {}
	}
#if UNITY_EDITOR
	public List<PlayerController>			players;
	public List<EggController>				eggs;
#else
	public List<PlayerController>			players { get; private set; }
	public List<EggController>				eggs { get; private set; }
#endif
	public GridController					gridController { get; private set; }
	public TeamManager						teamManager { get; private set; }
	public IPlayerInstantiationController	pic { get; private set; }
	public IEggInstantiationController		eic { get; private set; }
	public delegate void 					OnAPlayerEventHandler(OnAPlayerEventArgs ev);
	public event OnAPlayerEventHandler 		OnNewPlayer;
	public event OnAPlayerEventHandler 		OnAPlayerDying;

	PlayerManager(){}

	public PlayerManager(GridController gridController, TeamManager teamManager, IPlayerInstantiationController pic, IEggInstantiationController eic)
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
		var tmp = new List<PlayerController>();

		foreach (PlayerController player in players)
		{
			if (player.team == team && !player.dead)
				tmp.Add (player);
		}
		if (tmp.Count == 0)
			return null;
		return tmp.ToArray ();
	}

	public PlayerController[] GetPlayersInSquare(ISquare square)
	{
		var tmp = new List<PlayerController>();
		
		foreach (PlayerController player in players)
		{
			if (player.oldSquare == square && !player.dead)
				tmp.Add (player);
		}
		return tmp.ToArray ();
	}

	public EggController[] GetEggsInSquare(ISquare square)
	{
		var tmp = new List<EggController>();

		foreach (var egg in eggs)
		{
			if (egg.currentSquare == square && !egg.dead)
				tmp.Add (egg);
		}
		return tmp.ToArray ();
	}

	public virtual PlayerController SetPlayerConnection(int n, int x, int y, Orientation o, int l, string name)
	{
		if (players.Find (pl => pl.index == n) != null)
			throw new TwoPlayersWithTheSameIndexException();
		PlayerController player = pic.InstantiatePlayer();
		try
		{
			player.Init (x, y, o, l, n, teamManager.FindTeam(name), gridController);
		}
		catch (Exception e)
		{
			player.Destroy ();
			throw e;
		}
		players.Add (player);
		if (OnNewPlayer != null)
			OnNewPlayer( new OnAPlayerEventArgs {player = player});
		return player;
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
		player.SetInventory(nourriture, linemate, deraumere, sibur, mendiane, phiras, thystame);
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
		var square = GameManagerScript.instance.gridController.GetSquare (x, y);
		var players = GetPlayersInSquare (square);
		foreach (var player in players)
			player.StopIncantating ();
		if (incantationResult == 1 && (players[0] != null))
			GameManagerScript.instance.InstantiateIncantation (new Vector3(square.GetPosition ().x, 0, square.GetPosition ().z), players[0].team.color);
		return players.ToList ();
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
		if (OnAPlayerDying != null)
			OnAPlayerDying( new OnAPlayerEventArgs {player = player});
		return player;
	}

	public virtual EggController SetEggCreation(int e, int n, int x, int y)
	{
		if (eggs.Find (eg => eg.index == e) != null)
			throw new TwoEggsWithTheSameIndexException();
		EggController egg = this.eic.InstantiateEgg();
		PlayerController player = GetPlayer (n);
		player.StopLayingEgg ();
		try
		{
			egg.Init (x, y, e, player, gridController);
		}
		catch (Exception exc)
		{
			egg.Destroy();
			throw exc;
		}
		eggs.Add (egg);
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
		eggs.Remove (egg);
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

public class OnAPlayerEventArgs : System.EventArgs
{
	public PlayerController player;
}
