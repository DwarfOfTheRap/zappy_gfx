using UnityEngine;
using System.Collections;

public class ExitGameButton : MonoBehaviour {
    public void ExitGame()
    {
        TCPConnection.closeSocket();
        Application.Quit();
    }
}
