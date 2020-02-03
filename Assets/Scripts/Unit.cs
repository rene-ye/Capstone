using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Unit
{
    public const int WEIGHT_DEFAULT = 5;
    public const int WEIGHT_MAX = 99;
    public const int WEIGHT_ALLY = 50;

    public int health, attack, mana, range, cost;
    public int tier = 1;
    public string unit_name, unit_asset_location;
    // frequency of bullets generated
    public float attackSpeed = 1;
    // velocity of the bullet
    public float projectileSpeed = 1;
    // weight of the unit
    public int weight = WEIGHT_DEFAULT;

    public bool isAlly = true;

    public int currentHealth, currentMana;

    private float timeOfLastAttack = 0.0f;

    public void rankUp()
    {
        if (this.tier < 3)
        {
            this.health = this.health * 2;
            this.attack = this.attack * 2;
            this.tier++;
        }
    }

    public static bool sameType(Unit a, Unit b)
    {
        if (null == a || null == b)
            return false;
        return a.unit_name.Equals(b.unit_name) && (a.tier == b.tier);
    }

    static Color[] colors = new Color[] { Color.white, Color.yellow, Color.red };
    public Color getTierColor()
    {
        return colors[tier -1];
    }

    public virtual void resetForCombat()
    {
        currentHealth = health;
        currentMana = 0;
    }

    public bool readyToAttack()
    {
        if (Time.time - timeOfLastAttack > attackSpeed)
        {
            timeOfLastAttack = Time.time;
            return true;
        }
        return false;
    }

    public virtual int takeDamage(int attack)
    {
        currentHealth -= attack;
        return attack;
    }

    public virtual void dealDamage(Unit u)
    {
        u.takeDamage(attack);
    }
}


[Serializable]
public class UnitInfo
{
    [SerializeField]
    public string unit_name;
    [SerializeField]
    public int unit_tier;
    [SerializeField]
    public int locationX;
    [SerializeField]
    public int locationY;

    public UnitInfo(string name, int tier, int x, int y)
    {
        unit_name = name;
        unit_tier = tier;
        locationX = x;
        locationY = y;
    }
}

public static class UnitFactory
{

    public static Unit rollUnit(Player p)
    {
        float[] unitTierRollChances = getChances(p.getLevel());
        int unitTierToRoll = RandomUtils.roll(unitTierRollChances) + 1;
        float[] playerSpecificUnitRates = p.getRatesForUnitTier(unitTierToRoll);
        int unitIndex = RandomUtils.roll(playerSpecificUnitRates);
        p.updateRatesForUnitTier(unitTierToRoll, unitIndex);
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

    static float[] one = new float[] { 100, 0, 0, 0, 0 };
    static float[] two = new float[] { 70, 30, 0, 0, 0 };
    static float[] three = new float[] { 60, 35, 5, 0, 0 };
    static float[] four = new float[] { 50, 35, 15, 0, 0 };
    static float[] five = new float[] { 40, 35, 23, 2, 0 };
    static float[] six = new float[] { 33, 30, 30, 7, 0 };
    static float[] seven = new float[] { 30, 30, 30, 10, 0 };
    static float[] eight = new float[] { 24, 30, 30, 15, 1 };
    static float[] nine = new float[] { 22, 30, 30, 20, 3 };
    static float[] ten = new float[] { 19, 25, 25, 25, 6 };
    private static float[] getChances(int level)
    {

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

    public static Unit createUnit(string name)
    {
        switch (name)
        {
            case "Axe":
                return new Axe();
            case "Bounty":
                return new Bounty();
            case "Ogre":
                return new Ogre();
            case "Clock":
                return new Clock();
            case "Walrus":
                return new Walrus();
            case "Inventor":
                return new Inventor();
            case "Hunter":
                return new Hunter();
            case "Exorcist":
                return new Exorcist();
            case "Rider":
                return new Rider();
            case "Enchantress":
                return new Enchantress();
            case "Maiden":
                return new Maiden();
            case "Blademaster":
                return new Blademaster();
            case "Queen":
                return new Queen();
            case "Elemental":
                return new Elemental();
            case "Prophet":
                return new Prophet();
            case "Fiend":
                return new Fiend();
            case "Sniper":
                return new Sniper();
            case "Gandalf":
                return new Gandalf();
            case "Wolverine":
                return new Wolverine();
            case "Admiral":
                return new Admiral();
            case "Troll":
                return new Troll();
            case "Medusa":
                return new Medusa();
            case "Zeus":
                return new Zeus();
            case "Dematerializer":
                return new Dematerializer();
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
        float rollResult = UnityEngine.Random.Range(0.0f, 100.0f);
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
 */
public class Axe : Unit
{
    public Axe()
    {
        unit_name = "Axe";
        unit_asset_location = "Tiles/Axe";

        range = 1;
        cost = 1;
        attackSpeed = 1.5f;

        health = 700;
        attack = 50;
        mana = 50;
    }

    public override void dealDamage(Unit u)
    {
        base.dealDamage(u);
        if (currentMana < mana)
            currentMana += 5;
    }

    public override int takeDamage(int attack)
    {
        if (currentMana < mana)
        {
            currentMana += 5;
            return base.takeDamage(attack);
        } else
        {
            return base.takeDamage(attack / 2);
        }
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
        attackSpeed = 1.1f;

        health = 550;
        attack = 70;
        mana = 0;
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
        attackSpeed = 1.4f;

        health = 700;
        attack = 85;
        mana = 0;
    }
}
public class Clock : Unit
{
    public Clock()
    {
        unit_name = "Clock";
        unit_asset_location = "Tiles/Clock";

        range = 2;
        cost = 1;
        attackSpeed = 1.4f;

        health = 700;
        attack = 55;
        mana = 0;
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
        attackSpeed = 1.2f;

        health = 650;
        attack = 55;
        mana = 0;
    }
}
public class Inventor : Unit
{
    public Inventor()
    {
        unit_name = "Inventor";
        unit_asset_location = "Tiles/Inventor";

        range = 3;
        cost = 1;
        attackSpeed = 1.5f;

        health = 400;
        attack = 50;
        mana = 20;
    }

    public override void dealDamage(Unit u)
    {
        if (currentMana < mana)
        {
            base.dealDamage(u);
            currentMana += 5;
        }
        else
        {
            u.takeDamage(attack * 5);
            currentMana = 0;
        }
    }
}
public class Hunter : Unit
{
    public Hunter()
    {
        unit_name = "Hunter";
        unit_asset_location = "Tiles/Hunter";

        range = 5;
        cost = 1;
        attackSpeed = 1.3f;

        health = 400;
        attack = 50;
        mana = 0;
    }
}
public class Exorcist : Unit
{
    public Exorcist()
    {
        unit_name = "Exorcist";
        unit_asset_location = "Tiles/Exorcist";

        range = 3;
        cost = 1;
        attackSpeed = 1.4f;

        health = 550;
        attack = 75;
        mana = 0;
    }
}

public class Enchantress : Unit
{
    public Enchantress()
    {
        unit_name = "Enchantress";
        unit_asset_location = "Tiles/Enchantress";

        range = 3;
        cost = 1;
        attackSpeed = 1.5f;

        health = 400;
        attack = 60;
        mana = 100;
    }

    public override void dealDamage(Unit u)
    {
        if (currentMana < mana)
        {
            base.dealDamage(u);
            currentMana += 5;
        }
        else
        {
            currentHealth += health / 10;
            if (currentHealth > health)
                currentHealth = health;
            base.dealDamage(u);
        }
    }
    public override int takeDamage(int attack)
    {
        if (currentMana < mana)
            currentMana += 5;
        return base.takeDamage(attack);
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
        attackSpeed = 1.5f;

        health = 600;
        attack = 80;
        mana = 0;
    }
    public override void dealDamage(Unit u)
    {
        u.takeDamage(Mathf.CeilToInt(attack * 1.5f));
    }
    public override int takeDamage(int attack)
    {
        return base.takeDamage(Mathf.CeilToInt(attack * 1.5f));
    }
}
public class Maiden : Unit
{
    private Battlefield bf = null;

    public Maiden()
    {
        unit_name = "Maiden";
        unit_asset_location = "Tiles/Maiden";

        range = 3;
        cost = 2;
        attackSpeed = 1.7f;

        health = 450;
        attack = 45;
        mana = 50;
    }
    public override void dealDamage(Unit u)
    {
        if (currentMana < mana)
            currentMana += 5;
        base.dealDamage(u);
    }
    public override int takeDamage(int attack)
    {
        if (currentMana < mana)
            currentMana += 5;
        else
        {
            if (bf == null)
                bf = GameObject.Find("Board").GetComponent<Battlefield>();
            foreach (BaseTileHandler b in bf.tileMap.Values)
            {
                Unit u = b.getCurrentUnit();
                if (u != null && (u.isAlly == this.isAlly) && (u.mana > 0))
                {
                    u.currentMana += 50;
                    if (u.currentMana > u.mana)
                        u.currentMana = u.mana;
                }
            }
            currentMana = 0;
        }
        return base.takeDamage(attack);
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
        attackSpeed = 1.1f;

        health = 600;
        attack = 70;
        mana = 0;
    }
}
public class Queen : Unit
{
    public Queen()
    {
        unit_name = "Queen";
        unit_asset_location = "Tiles/Queen";

        range = 3;
        cost = 2;
        attackSpeed = 1.2f;

        health = 550;
        attack = 90;
        mana = 0;
    }

    public override void dealDamage(Unit u)
    {
        u.takeDamage(Mathf.CeilToInt(attack * 1.5f));
    }
    public override int takeDamage(int attack)
    {
        return base.takeDamage(Mathf.CeilToInt(attack * 1.5f));
    }
}
public class Elemental : Unit
{
    public Elemental()
    {
        unit_name = "Elemental";
        unit_asset_location = "Tiles/Elemental";

        range = 3;
        cost = 2;
        attackSpeed = 1.3f;

        health = 550;
        attack = 65;
        mana = 0;
    }
}
public class Prophet : Unit
{
    public Prophet()
    {
        unit_name = "Prophet";
        unit_asset_location = "Tiles/Prophet";

        range = 3;
        cost = 2;
        attackSpeed = 1.4f;

        health = 500;
        attack = 50;
        mana = 0;
    }
}
public class Fiend : Unit
{
    public Fiend()
    {
        unit_name = "Fiend";
        unit_asset_location = "Tiles/Fiend";

        range = 3;
        cost = 3;
        attackSpeed = 1.1f;

        health = 500;
        attack = 90;
        mana = 0;
    }

    public override void dealDamage(Unit u)
    {
        u.takeDamage(Mathf.CeilToInt(attack * 2.0f));
    }
    public override int takeDamage(int attack)
    {
        return base.takeDamage(Mathf.CeilToInt(attack * 1.2f));
    }
}
public class Sniper : Unit
{
    public Sniper()
    {
        unit_name = "Sniper";
        unit_asset_location = "Tiles/Sniper";

        range = 8;
        cost = 3;
        attackSpeed = 1.1f;

        health = 450;
        attack = 85;
        mana = 0;
    }
}
public class Gandalf : Unit
{
    private Battlefield bf = null;

    public Gandalf()
    {
        unit_name = "Gandalf";
        unit_asset_location = "Tiles/Gandalf";

        range = 3;
        cost = 3;
        attackSpeed = 1.7f;

        health = 650;
        attack = 50;
        mana = 0;
    }

    public override void dealDamage(Unit unit)
    {
        if (bf == null)
            bf = GameObject.Find("Board").GetComponent<Battlefield>();
        foreach (BaseTileHandler b in bf.tileMap.Values)
        {
            Unit u = b.getCurrentUnit();
            if (u != null && (u.isAlly == this.isAlly) && (u.mana > 0))
            {
                u.currentMana += 5;
                if (u.currentMana > u.mana)
                    u.currentMana = u.mana;
            }
        }
        base.dealDamage(unit);
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
        attackSpeed = 1.1f;

        health = 750;
        attack = 60;
        mana = 0;
    }

    public override void dealDamage(Unit u)
    {
        currentHealth += Mathf.CeilToInt(u.takeDamage(attack) * 0.2f);
        if (currentHealth > health)
            currentHealth = health;
    }
    public override int takeDamage(int attack)
    {
        return base.takeDamage(Mathf.CeilToInt(attack * 0.8f));
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
        attackSpeed = 1.4f;

        health = 950;
        attack = 90;
        mana = 0;
    }
}
public class Troll : Unit
{
    public Troll()
    {
        unit_name = "Troll";
        unit_asset_location = "Tiles/Troll";

        range = 2;
        cost = 4;
        attackSpeed = 1.1f;

        health = 900;
        attack = 100;
        mana = 0;
    }

    public override void dealDamage(Unit u)
    {
        base.dealDamage(u);
        if (attackSpeed > 0.3f)
        {
            attackSpeed -= 0.1f;
        }
    }

    public override void resetForCombat()
    {
        base.resetForCombat();
        attackSpeed = 1.1f;
    }
}
public class Medusa : Unit
{
    public Medusa()
    {
        unit_name = "Medusa";
        unit_asset_location = "Tiles/Medusa";

        range = 2;
        cost = 4;
        attackSpeed = 1.0f;

        health = 750;
        attack = 70;
        mana = 0;
    }
}
public class Zeus : Unit
{
    private Battlefield bf = null;

    public Zeus()
    {
        unit_name = "Zeus";
        unit_asset_location = "Tiles/Zeus";

        range = 3;
        cost = 5;
        attackSpeed = 1.4f;

        health = 950;
        attack = 70;
        mana = 100;
    }

    public override void dealDamage(Unit unit)
    {
        if (currentMana < mana)
            currentMana += 10;
        else
        {
            if (bf == null)
                bf = GameObject.Find("Board").GetComponent<Battlefield>();
            foreach (BaseTileHandler b in bf.tileMap.Values)
            {
                Unit u = b.getCurrentUnit();
                if (u != null && (u.isAlly != this.isAlly))
                {
                    float damage = 0.2f * tier;
                    u.takeDamage(u.currentHealth - Mathf.CeilToInt(u.health * damage));
                }
                mana = 0;
            }
        }
        base.dealDamage(unit);
    }

    public override int takeDamage(int attack)
    {
        mana += 10;
        return base.takeDamage(attack);
    }
}
public class Dematerializer : Unit
{
    public Dematerializer()
    {
        unit_name = "Dematerializer";
        unit_asset_location = "Tiles/Dematerializer";

        range = 3;
        cost = 5;
        attackSpeed = 1.5f;

        health = 1000;
        attack = 150;
        mana = 0;
    }
}