using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager {
	public class PlayerNotFoundException : Exception
	{
		public PlayerNotFoundException() {}

		public PlayerNotFoundException(string message) : base(message) {}
	}
	private Dictionary<int, PlayerController> players;
	private GridController gridController;

	private PlayerManager() {}

	public PlayerManager(GridController gridController)
	{
		this.players = new Dictionary<int, PlayerController>();
		this.gridController = gridController;
	}

	public virtual PlayerController GetPlayer(int n)
	{
		try
		{
			return players[n];
		}
		catch (Exception)
		{
			throw new PlayerNotFoundException();
		}
	}
	public PlayerController SetPlayerConnection(int n, int x, int y, Orientation o, int l, string name)
	{
		throw new NotImplementedException();
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
