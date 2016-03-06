using UnityEngine;
using System.Collections;

public class PortInputField : MonoBehaviour {

    public void ConnectToPortOnAddress(int arg0)
    {
        SocketManager.instance.conPort = arg0;
        Application.LoadLevel(1);
    }
}
