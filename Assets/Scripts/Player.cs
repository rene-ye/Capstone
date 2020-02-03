using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Slider slider, healthSlider;
    public Text levelTextBox, healthTextBox;
    public Field field;
    public Text goldText;
    public SupplyManager supply;
    public GameOver gameOverScreen;

    int level, gold, exp, health = 100;
    float[][] tiers = new float[][]
    {
        new float[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        new float[] { 20, 20, 20, 20, 20 },
        new float[] { 25, 25, 25, 25 },
        new float[] { 100 / 3, 100 / 3, 100 / 3 },
        new float[] { 50, 50 }
    };

    static int[] expRequired = new int[] { 1, 2, 4, 8, 12, 16, 20, 24, 32, 40 };
    public const int MAX_LEVEL = 10;

    public Player()
    {
        level = 1;
        gold = 30;
        exp = 0;
    }

    public void Update()
    {
        goldText.text = gold.ToString();
    }

    public int getLevel() { return level; }

    public float[] getRatesForUnitTier(int unitTier)
    {
        return tiers[unitTier - 1];
    }

    public void updateRatesForUnitTier(int unitTier, int unitIndex)
    {
        for (int i = 0; i < tiers[unitTier - 1].Length; i++)
        {
            if (i != unitIndex)
                tiers[unitTier - 1][i] += (1 / tiers[unitTier - 1].Length) / (tiers[unitTier - 1].Length - 1);
            else
                tiers[unitTier - 1][i] -= (1 / tiers[unitTier - 1].Length);
        }
        return;
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

    public void gainExp(int i)
    {
        if (this.level < MAX_LEVEL)
        {
            this.exp += i;
            checkLevel();
            slider.value = (float)this.exp / expRequired[this.level - 1];
        }
    }

    public int getExp()
    {
        return exp;
    }

    private void checkLevel()
    {
        int required = expRequired[level - 1];
        while(this.exp >= required)
        {
            this.level++;
            supply.increaseMaxSupply();
            levelTextBox.text = this.level.ToString();
            this.exp -= required;
            required = expRequired[level - 1];
        }
    }

    internal void takeDamage(int v)
    {
        health -= v;
        if (health <= 0)
        {
            gameOverScreen.gameObject.SetActive(true);
            if (PhotonNetwork.isMasterClient)
            {
                foreach (PhotonPlayer p in PhotonNetwork.playerList)
                {
                    if (!p.IsMasterClient)
                    {
                        PhotonNetwork.SetMasterClient(p);
                    }
                }
            }
            PhotonNetwork.LeaveRoom();
            GameObject.Find("GameMaster").GetComponent<HexGM>().startTimer = false;
        }
        healthSlider.value = health;
        healthTextBox.text = health.ToString();
    }

    public void addInterest()
    {
        int interest = Mathf.FloorToInt(gold / 10.0f);
        if (interest > 5)
        {
            interest = 5;
        }
        gold += 2 + interest;
    }

    public void spend(int i)
    {
        gold -= i;
    }

    public void gain(int i)
    {
        gold += i;
    }

    private Unit activeUnit = null;
    public GameObject activeUnitObject = null;

    public delegate void ResetGameObject();
    public ResetGameObject rgo;

    public void SetActiveUnit(Unit u, GameObject go)
    {
        if (activeUnit != null)
        {
            // unhighlight the last active unit
            activeUnitObject.GetComponent<Image>().color = activeUnit.getTierColor();
        }
        activeUnit = u;
        activeUnitObject = go;
        activeUnitObject.GetComponent<Image>().color = Color.magenta;
    }

    public Unit getActiveUnit()
    {
        return activeUnit;
    }

    public void clearActiveUnit()
    {
        activeUnit = null;
        activeUnitObject.GetComponent<Image>().color = Color.white;
        activeUnitObject = null;
    }

    public UnitInfo[] currentUnits;
    public void lockBoard()
    {
        currentUnits = field.lockAllyBoard().ToArray();
    }

    public void setEnemyBoard(UnitInfo[] u)
    {
        field.setEnemies(u);
    }

    public void switchRounds(int roundID)
    {
        switch (roundID)
        {
            case 0:
                field.setShopping();
                break;
            case 1:
                field.setIntermission();
                break;
            case 2:
                field.setBattle();
                break;
            default:
                break;
        }
    }
}