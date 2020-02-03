using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class UnitSpritePool
{
    static Dictionary<string, Sprite> pool;
    static Sprite emptyShop = Resources.Load<Sprite>("Tiles/EmptyHex");
    static Sprite emptyBench = Resources.Load<Sprite>("Tiles/WhiteHex");

    static UnitSpritePool()
    {
        // First instantialize our map
        pool = new Dictionary<string, Sprite>();
        // Ok now we have to load and assign our sprites
        string[] unitList = { "Axe", "Bounty", "Ogre", "Clock", "Walrus", "Inventor", "Hunter", "Exorcist",
            "Rider", "Enchantress", "Maiden", "Blademaster", "Queen", "Elemental", "Prophet", "Fiend", "Sniper", "Gandalf",
            "Wolverine", "Admiral", "Troll", "Medusa", "Zeus", "Dematerializer" };
        foreach (string str in unitList)
        {
            Sprite s = Resources.Load<Sprite>("Tiles/" + str);
            pool.Add(str, s);
        }
    }

    public static Sprite getSprite(string unitName)
    {
        if (pool.ContainsKey(unitName))
        {
            return pool[unitName];
        }
        return null;
    }

    public static Sprite getDefaultShop()
    {
        return emptyShop;
    }
    public static Sprite getDefaultBench()
    {
        return emptyBench;
    }
}