using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public GameObject p1, p2, p3, p4;
    // Start is called before the first frame update
    void Start()
    {
        p1.SetActive(true);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
    }

    public void setPlayerActiveState(int player, bool state)
    {
        switch (player)
        {
            case 1:
                p1.SetActive(state);
                break;
            case 2:
                p2.SetActive(state);
                break;
            case 3:
                p3.SetActive(state);
                break;
            case 4:
                p4.SetActive(state);
                break;
            default:
                Debug.Log("Player " + player + " not found.");
                break;
        }
    }
}
