using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit
{
    public int health, attack, mana, range, cost;
    public string unit_name, unit_asset_location;
}

public static class UnitFactory
{

}


/*
 * Unit list starts here
 * 
 */
public class Axe : Unit
{
    Axe()
    {
        unit_name = "Axe";
        unit_asset_location = "Tiles/Axe";

        range = 1;
        cost = 1;

        health = 700;
        attack = 60;
        mana = 100;
    }
}

public class Bounty : Unit
{
    Bounty()
    {
        unit_name = "Bounty";
        unit_asset_location = "Tiles/Bounty";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}