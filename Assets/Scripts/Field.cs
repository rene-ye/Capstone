using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public Text roundText, timer;
    public GameObject shop;

    public Player p;
    public Board ally, enemy;
    public Battlefield battlefield;
    public RefreshButtonHandler rbh;

    private bool isShowing = true;
    // Start is called before the first frame update
    void Start()
    {
        enemy.transform.localScale = Vector3.zero;
        setState();
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = Mathf.FloorToInt((float)HexGM.getRoundTimer()).ToString();
    }

    public void setState()
    {
        isShowing = !isShowing;
        if (isShowing)
        {
            shop.transform.localScale = Vector3.zero;
            ally.transform.localScale = Vector3.one;
            if (!HexGM.isShoppingRound())
            {
                enemy.transform.localScale = Vector3.one;
            }
        }
        else
        {
            shop.transform.localScale = Vector3.one;
            ally.transform.localScale = Vector3.zero;
            if (!HexGM.isShoppingRound())
            {
                enemy.transform.localScale = Vector3.zero;
            }
        }
    }

    public void setShopping()
    {
        roundText.text = "Shopping";
        if (isShowing)
        {
            enemy.transform.localScale = Vector3.zero;
        }
        endBattleRoundTasks();
    }

    public void setIntermission()
    {
        roundText.text = "Getting Ready";
    }

    public void setBattle()
    {
        battlefield.initAllUnits();
        if (p.getActiveUnit() != null)
        {
            Image img = p.activeUnitObject.GetComponent<Image>();
            Color c = p.getActiveUnit().getTierColor();
            p.clearActiveUnit();
            img.color = c;
        }
        roundText.text = "Battle";
        if (isShowing)
        {
            enemy.transform.localScale = Vector3.one;
        }
    }

    private void endBattleRoundTasks()
    {
        p.takeDamage(calculateDamage());
        p.addInterest();
        rbh.rerollShop(0);
        p.gainExp(1);
        ally.revertToLocked();
        enemy.clear();
    }

    private int calculateDamage()
    {
        int damage = 0;
        foreach (BaseTileHandler b in Battlefield.tileMap.Values)
        {
            Unit u = b.getCurrentUnit();
            if (u != null && !u.isAlly)
            {
                damage += u.cost + (u.cost * (u.tier - 1));
            }
        }
        return damage;
    }

    public List<UnitInfo> lockAllyBoard()
    {
        ally.lockBoard();
        return ally.unitInfo;
    }

    public void setEnemies(UnitInfo[] u)
    {
        foreach (UnitInfo unitInfo in u)
        {
            Unit unit = UnitFactory.createUnit(unitInfo.unit_name);
            for(int i = unitInfo.unit_tier; i > 1; i--)
            {
                unit.rankUp();
            }
            unit.isAlly = false;

            int locationX = 12 - unitInfo.locationX;
            int locationY = (locationX % 2 == 0) ? (3 - unitInfo.locationY) : (2 - unitInfo.locationY);
            Battlefield.tileMap[locationX + "," + locationY].setUnit(unit);
        }
    }
}
