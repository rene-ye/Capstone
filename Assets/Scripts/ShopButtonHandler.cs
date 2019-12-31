using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonHandler : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;
    public PlayerInfo playerObj;

    public BenchManager benchManager;

    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }

    public void setUnit(Unit u)
    {
        unit = u;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getSprite(unit.unit_name);
    }

    public void buyUnit()
    {
        if (unit != null && playerObj.getPlayer().canAfford(unit.cost) && benchManager.addToBench(unit))
        {
            playerObj.getPlayer().spend(unit.cost);
            resetDefault();
        }
    }

    private void resetDefault()
    {
        this.unit = null;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getDefaultShop();
    }
}


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
            "Rider", "Enchantress", "Maiden", "Blademaster", "Queen", "Elemental", "Prophet", "Fiend", "Sniper",
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