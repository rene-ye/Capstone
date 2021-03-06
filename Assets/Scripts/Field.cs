﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    private static Vector3 shopScale = new Vector3(1.5f, 1.5f, 1f);
    private static Vector3 boardScale = Vector3.one;


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
            ally.transform.localScale = boardScale;
            if (!HexGM.isShoppingRound())
            {
                enemy.transform.localScale = boardScale;
            }
        }
        else
        {
            shop.transform.localScale = shopScale;
            ally.transform.localScale = Vector3.zero;
            if (!HexGM.isShoppingRound())
            {
                enemy.transform.localScale = Vector3.zero;
            }
        }
    }

    public void setShopping()
    {
        if (isShowing)
        {
            enemy.transform.localScale = Vector3.zero;
        }
        endBattleRoundTasks();
        roundText.text = "Shopping";
    }

    public void setIntermission()
    {
        if (PhotonNetwork.playerList.Length <= 1)
        {
            p.gameOverScreen.setWinner();
            p.gameOverScreen.gameObject.SetActive(true);
            GameObject.Find("GameMaster").GetComponent<HexGM>().startTimer = false;
        }
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
            enemy.transform.localScale = boardScale;
        }
    }

    private void endBattleRoundTasks()
    {
        p.takeDamage(calculateDamage());
        p.addInterest();
        rbh.rerollShop(0);
        p.gainExp(1);

        enemy.clear();
        ally.clear();
        setAllies();
    }

    private int calculateDamage()
    {
        int damage = 0;
        foreach (BaseTileHandler b in battlefield.tileMap.Values)
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

    public void setAllies()
    {
        foreach (UnitInfo unitInfo in ally.unitInfo)
        {
            Unit unit = UnitFactory.createUnit(unitInfo.unit_name);
            for (int i = unitInfo.unit_tier; i > 1; i--)
            {
                unit.rankUp();
            }
            unit.isAlly = true;

            int locationX = unitInfo.locationX;
            int locationY = unitInfo.locationY;
            battlefield.tileMap[locationX + "," + locationY].setUnit(unit);
        }
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
            battlefield.tileMap[locationX + "," + locationY].setUnit(unit);
        }
    }
}
