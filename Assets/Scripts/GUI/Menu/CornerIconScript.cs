using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CornerIconScript : MonoBehaviour {
	void OnEnable () {
		SocketManager.instance.OnReconnection += RefreshIcon;
	}

	void OnDisable () {
		SocketManager.instance.OnReconnection -= RefreshIcon;
	}

	void RefreshIcon ()
	{
		GetComponent<Animator>().SetTrigger ("Refresh");
	}
}
