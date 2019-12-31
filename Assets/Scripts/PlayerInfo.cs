using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int level, gold, exp;
    float[] tier1 = new float[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    float[] tier2 = new float[] { 20, 20, 20, 20, 20 };
    float[] tier3 = new float[] { 25, 25, 25, 25 };
    float[] tier4 = new float[] { 100 / 3, 100 / 3, 100 / 3 };
    float[] tier5 = new float[] { 50, 50 };


    public Player()
    {
        level = 1;
        gold = 1000;
        exp = 0;
    }

    public int getLevel() { return level; }

    public float[] getRatesForUnitTier(int unitTier)
    {
        switch (unitTier)
        {
            case 1:
                return tier1;
            case 2:
                return tier2;
            case 3:
                return tier3;
            case 4:
                return tier4;
            case 5:
                return tier5;
            default:
                return null;
        }
    }

    public bool canAfford(int i)
    {
        if (gold >= i)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void spend(int i)
    {
        gold -= i;
    }
}

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
