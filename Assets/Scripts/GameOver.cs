using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public UnityEngine.UI.Text text;

    public void LeaveRoom()
    {
        if (PhotonNetwork.inRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        PhotonNetwork.LoadLevel(1);
    }

    public void setWinner()
    {
        text.text = "You Won";
    }
}
