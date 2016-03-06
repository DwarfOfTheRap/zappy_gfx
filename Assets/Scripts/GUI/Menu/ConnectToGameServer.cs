using UnityEngine;
using System.Collections;

public class ConnectToGameServer : MonoBehaviour {

    public void Connect()
    {

    }

    public void DisplayWindow()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void CloseWindow()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}