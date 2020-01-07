using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public Player p;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        if (p.canAfford(4) && p.getLevel() < Player.MAX_LEVEL)
        {
            p.spend(5);
            p.gainExp(4);
        }
    }
}
