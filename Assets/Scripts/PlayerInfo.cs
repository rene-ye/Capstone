using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    Player p;

    // Start is called before the first frame update
    void Start()
    {
        p = new Player();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player getPlayer()
    {
        return p;
    }
}
