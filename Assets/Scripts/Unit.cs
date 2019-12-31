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

    public static Unit rollUnit(Player p)
    {
        float[] unitTierRollChances = getChances(p.getLevel());
        int unitTierToRoll = RandomUtils.roll(unitTierRollChances) + 1;
        float[] playerSpecificUnitRates = p.getRatesForUnitTier(unitTierToRoll);
        int unitIndex = RandomUtils.roll(playerSpecificUnitRates);
        return createUnit(unitTierToRoll, unitIndex);
    }

    private static Unit createUnit(int unitTier, int unitIndex)
    {
        switch (unitTier)
        {
            case 1:
                return getTier1Unit(unitIndex);
            case 2:
                return getTier2Unit(unitIndex);
            case 3:
                return getTier3Unit(unitIndex);
            case 4:
                return getTier4Unit(unitIndex);
            case 5:
                return getTier5Unit(unitIndex);
            default:
                return null;
        }
    }

    private static float[] getChances(int level)
    {
        float[] one = new float[] { 100, 0, 0, 0, 0 };
        float[] two = new float[] { 70, 30, 0, 0, 0 };
        float[] three = new float[] { 60, 35, 5, 0, 0 };
        float[] four = new float[] { 50, 35, 15, 0, 0 };
        float[] five = new float[] { 40, 35, 23, 2, 0 };
        float[] six = new float[] { 33, 30, 30, 7, 0 };
        float[] seven = new float[] { 30, 30, 30, 10, 0 };
        float[] eight = new float[] { 24, 30, 30, 15, 1 };
        float[] nine = new float[] { 22, 30, 30, 20, 3 };
        float[] ten = new float[] { 19, 25, 25, 25, 6 };

        switch (level)
        {
            case 1:
                return one;
            case 2:
                return two;
            case 3:
                return three;
            case 4:
                return four;
            case 5:
                return five;
            case 6:
                return six;
            case 7:
                return seven;
            case 8:
                return eight;
            case 9:
                return nine;
            case 10:
                return ten;
            default:
                return null;
        }
    }

    private static Unit getTier1Unit(int unitIndex)
    {
        switch (unitIndex)
        {
            case 0:
                return new Axe();
            case 1:
                return new Bounty();
            case 2:
                return new Ogre();
            case 3:
                return new Clock();
            case 4:
                return new Walrus();
            case 5:
                return new Inventor();
            case 6:
                return new Hunter();
            case 7:
                return new Exorcist();
            case 8:
                return new Rider();
            case 9:
                return new Enchantress();
            default:
                return null;
        }
    }

    private static Unit getTier2Unit(int unitIndex)
    {
        switch (unitIndex)
        {
            case 0:
                return new Maiden();
            case 1:
                return new Blademaster();
            case 2:
                return new Queen();
            case 3:
                return new Elemental();
            case 4:
                return new Prophet();
            default:
                return null;
        }
    }

    private static Unit getTier3Unit(int unitIndex)
    {
        switch (unitIndex)
        {
            case 0:
                return new Fiend();
            case 1:
                return new Sniper();
            case 2:
                return new Gandalf();
            case 3:
                return new Wolverine();
            default:
                return null;
        }
    }

    private static Unit getTier4Unit(int unitIndex)
    {
        switch (unitIndex)
        {
            case 0:
                return new Admiral();
            case 1:
                return new Troll();
            case 2:
                return new Medusa();
            default:
                return null;
        }
    }
    private static Unit getTier5Unit(int unitIndex)
    {
        switch (unitIndex)
        {
            case 0:
                return new Zeus();
            case 1:
                return new Dematerializer();
            default:
                return null;
        }
    }
}

public static class RandomUtils
{
    // Returns the index of the roll
    public static int roll(float[] d)
    {
        float rollResult = Random.Range(0.0f, 100.0f);
        float cumulative = 0.0f;
        for (int i = 0; i < d.Length; i++)
        {
            cumulative += d[i];
            if (rollResult < cumulative)
            {
                return i;
            }
        }
        // Use the last non zero result
        for (int i = d.Length; i > 0; i--)
        {
            if (d[i-1] > 0)
            {
                return i - 1;
            }
        }
        // Should never get here
        return 0;
    }
}


/*
 * Unit list starts here
 * 
 */
public class Axe : Unit
{
    public Axe()
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
    public Bounty()
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
public class Ogre : Unit
{
    public Ogre()
    {
        unit_name = "Ogre";
        unit_asset_location = "Tiles/Ogre";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Clock : Unit
{
    public Clock()
    {
        unit_name = "Clock";
        unit_asset_location = "Tiles/Clock";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Walrus : Unit
{
    public Walrus()
    {
        unit_name = "Walrus";
        unit_asset_location = "Tiles/Walrus";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Inventor : Unit
{
    public Inventor()
    {
        unit_name = "Inventor";
        unit_asset_location = "Tiles/Inventor";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Hunter : Unit
{
    public Hunter()
    {
        unit_name = "Hunter";
        unit_asset_location = "Tiles/Hunter";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Exorcist : Unit
{
    public Exorcist()
    {
        unit_name = "Exorcist";
        unit_asset_location = "Tiles/Exorcist";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Rider : Unit
{
    public Rider()
    {
        unit_name = "Rider";
        unit_asset_location = "Tiles/Rider";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Enchantress : Unit
{
    public Enchantress()
    {
        unit_name = "Enchantress";
        unit_asset_location = "Tiles/Enchantress";

        range = 1;
        cost = 1;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Maiden : Unit
{
    public Maiden()
    {
        unit_name = "Maiden";
        unit_asset_location = "Tiles/Maiden";

        range = 1;
        cost = 2;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Blademaster : Unit
{
    public Blademaster()
    {
        unit_name = "Blademaster";
        unit_asset_location = "Tiles/Blademaster";

        range = 1;
        cost = 2;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Queen : Unit
{
    public Queen()
    {
        unit_name = "Queen";
        unit_asset_location = "Tiles/Queen";

        range = 1;
        cost = 2;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Elemental : Unit
{
    public Elemental()
    {
        unit_name = "Elemental";
        unit_asset_location = "Tiles/Elemental";

        range = 1;
        cost = 2;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Prophet : Unit
{
    public Prophet()
    {
        unit_name = "Prophet";
        unit_asset_location = "Tiles/Prophet";

        range = 1;
        cost = 2;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Fiend : Unit
{
    public Fiend()
    {
        unit_name = "Fiend";
        unit_asset_location = "Tiles/Fiend";

        range = 1;
        cost = 3;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Sniper : Unit
{
    public Sniper()
    {
        unit_name = "Sniper";
        unit_asset_location = "Tiles/Sniper";

        range = 1;
        cost = 3;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Gandalf : Unit
{
    public Gandalf()
    {
        unit_name = "Gandalf";
        unit_asset_location = "Tiles/Gandalf";

        range = 1;
        cost = 3;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Wolverine : Unit
{
    public Wolverine()
    {
        unit_name = "Wolverine";
        unit_asset_location = "Tiles/Wolverine";

        range = 1;
        cost = 3;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Admiral : Unit
{
    public Admiral()
    {
        unit_name = "Admiral";
        unit_asset_location = "Tiles/Admiral";

        range = 1;
        cost = 4;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Troll : Unit
{
    public Troll()
    {
        unit_name = "Troll";
        unit_asset_location = "Tiles/Troll";

        range = 1;
        cost = 4;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Medusa : Unit
{
    public Medusa()
    {
        unit_name = "Medusa";
        unit_asset_location = "Tiles/Medusa";

        range = 1;
        cost = 4;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Zeus : Unit
{
    public Zeus()
    {
        unit_name = "Zeus";
        unit_asset_location = "Tiles/Zeus";

        range = 1;
        cost = 5;

        health = 550;
        attack = 85;
        mana = 100;
    }
}
public class Dematerializer : Unit
{
    public Dematerializer()
    {
        unit_name = "Dematerializer";
        unit_asset_location = "Tiles/Dematerializer";

        range = 1;
        cost = 5;

        health = 550;
        attack = 85;
        mana = 100;
    }
}