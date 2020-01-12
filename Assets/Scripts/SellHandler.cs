using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellHandler : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sell(Unit u)
    {
        player.gain(u.cost);
    }

    public void onClick()
    {
        Unit u = player.getActiveUnit();
        if (u != null)
        {
            player.gain(u.cost);
            player.clearActiveUnit();
            player.rgo();
        }
    }
}
