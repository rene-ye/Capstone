using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    public void OnClick_Start()
    {
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
