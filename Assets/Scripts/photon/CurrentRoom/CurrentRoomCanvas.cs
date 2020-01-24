using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    public void OnClick_Start()
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
