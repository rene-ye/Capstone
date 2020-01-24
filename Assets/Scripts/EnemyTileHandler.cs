using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTileHandler : MonoBehaviour, BaseTileHandler
{
    public Slider healthBar, manaBar;
    public GameObject AllyBullet;
    public Battlefield battlefield;

    private Unit unit = null;
    private Vector2Int coordinate;
    private bool barsActive = false;
    private Image healthBarColor;

    // Start is called before the first frame update
    void Start()
    {
        activateBars(false);
        healthBarColor = healthBar.transform.Find("Fill Area").GetComponentInChildren<Image>();

        string[] s = gameObject.name.Split(',');
        int x = int.Parse(s[0]);
        int y = int.Parse(s[1]);
        coordinate = new Vector2Int(x, y);
    }
    private void setHealthColor(bool isAlly)
    {
        healthBarColor.color = isAlly ? Color.green : Color.red;
    }

    void Update()
    {
        if (HexGM.isBattleRound())
        {
            /*
             * Check whether to display health bars
             */
            if (unit != null && !barsActive)
            {
                activateBars(true);
            }

            if (barsActive && unit == null)
            {
                activateBars(false);
            }
            else if (barsActive && unit != null)
            {
                setHealthColor(unit.isAlly);
                healthBar.value = (float)unit.currentHealth / unit.health;
                manaBar.value = (float)unit.currentMana / unit.mana;
            }

            /*
             * Combat logic here
             */
            if (unit != null && unit.readyToAttack())
            {
                // figure out which tile to attack
                List<BaseTileHandler> bthl = battlefield.getClosestEnemy(coordinate, unit.isAlly);
                if (bthl != null)
                {
                    BaseTileHandler bth = bthl[0];
                    if (!(Battlefield.getDistance(this.coordinate, bth.getCoordinate()) <= unit.range))
                    {
                        // it's out of range, move instead, we already got the shortest path so try to move along the path
                        // the first unit is the target, so we want to start with the furthest possible range from the target
                        for (int i = unit.range; i > 0; i--)
                        {
                            if (bthl[i].getCurrentUnit() == null)
                            {
                                if (bthl[i].setUnit(this.unit))
                                {
                                    Debug.Log("Moving Inside range");
                                    this.resetDefault();
                                    return;
                                }
                            }
                        }
                        for (int i = unit.range; i < bthl.Count; i++)
                        {
                            if (bthl[i].getCurrentUnit() == null)
                            {
                                if (bthl[i].setUnit(this.unit))
                                {
                                    Debug.Log("Moving outside range");
                                    this.resetDefault();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Create the bullet, it'll be responsible for it's own destruction
                        var newBullet = Instantiate(AllyBullet, this.transform.position, Quaternion.identity);
                        newBullet.transform.SetParent(this.transform.parent.parent);
                        if (!unit.isAlly)
                        {
                            newBullet.GetComponent<Image>().color = Color.red;
                        }
                        BulletHandler b = newBullet.gameObject.GetComponent<BulletHandler>();
                        b.setDestination(this.transform.position, bth, unit);
                    }
                }
            }
        }
        else if (barsActive)
        {
            activateBars(false);
        }
    }

    private void activateBars(bool b)
    {
        barsActive = b;
        healthBar.gameObject.SetActive(b);
        manaBar.gameObject.SetActive(b);
    }

    public bool setUnit(Unit u)
    {
        if (this.unit == null)
        {
            unit = u;
            this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getSprite(unit.unit_name);
            this.gameObject.GetComponent<Image>().color = unit.getTierColor();
            return true;
        }
        return false;
    }

    public void resetDefault()
    {
        this.unit = null;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getDefaultBench();
        this.gameObject.GetComponent<Image>().color = Color.white;
        activateBars(false);
    }

    public Unit getCurrentUnit()
    {
        return unit;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public Vector2Int getCoordinate()
    {
        return coordinate;
    }

    public int getNodeWeight(bool isAlly)
    {
        if (unit == null)
        {
            return Unit.WEIGHT_DEFAULT;
        }
        else
        {
            if (isAlly == unit.isAlly)
                return Unit.WEIGHT_ALLY;
            return unit.weight;
        }
    }

    public void takeDamage(int attack)
    {
        if (!HexGM.isShoppingRound() && this.unit != null)
        {
            unit.currentHealth -= attack;
            if (unit.currentHealth <= 0)
            {
                this.resetDefault();
            }
        }
    }
}
