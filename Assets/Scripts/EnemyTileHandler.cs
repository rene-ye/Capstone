using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTileHandler : MonoBehaviour, BaseTileHandler
{
    public Slider healthBar, manaBar;
    private Unit unit = null;
    private Vector2Int coordinate;
    public GameObject AllyBullet;
    private bool barsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        activateBars(false);

        string[] s = gameObject.name.Split(',');
        int x = int.Parse(s[0]);
        int y = int.Parse(s[1]);
        coordinate = new Vector2Int(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!HexGM.isShoppingRound())
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
            } else if (barsActive && unit != null)
            {
                healthBar.value = (float)unit.currentHealth / unit.health;
                manaBar.value = (float)unit.currentMana / unit.mana;
            }

            /*
             * Combat logic here
             */
            if (unit != null && unit.readyToAttack())
            {
                //figure out which tile to attack
                BaseTileHandler bth = Battlefield.getClosestEnemy(coordinate, unit.isAlly);
                if (bth != null)
                {
                    Vector3 target = bth.getGameObject().transform.position;

                    //Create the bullet, it'll be responsible for it's own destruction
                    var newBullet = Instantiate(AllyBullet, this.transform.localPosition, Quaternion.identity);
                    newBullet.transform.SetParent(this.transform.parent.parent);
                    if (!unit.isAlly)
                    {
                        newBullet.GetComponent<Image>().color = Color.red;
                    }
                    BulletHandler b = newBullet.gameObject.GetComponent<BulletHandler>();
                    b.setDestination(this.transform.position, bth, unit);
                    newBullet.SetActive(true);
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

    public void setUnit(Unit u)
    {
        unit = u;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getSprite(unit.unit_name);
        this.gameObject.GetComponent<Image>().color = unit.getTierColor();
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

    public int getNodeWeight()
    {
        if (unit == null)
        {
            return Unit.WEIGHT_DEFAULT;
        }
        else
        {
            if (unit.isAlly)
                return Unit.WEIGHT_MAX;
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
