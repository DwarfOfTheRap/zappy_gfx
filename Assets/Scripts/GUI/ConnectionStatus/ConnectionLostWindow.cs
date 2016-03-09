using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConnectionLostWindow : MonoBehaviour {

    public void DisplayWindow()
    {
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void HideWindow()
    {
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

	void Update () {
        if (!SocketManager.instance.socketAvailable)
            DisplayWindow();
        else
            HideWindow();
	}
}
