﻿using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default);
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("Create room failed: " + codeAndMessage[1]);
    }
}
