using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int level, gold, exp;
    float[] tier1 = new float[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    float[] tier2 = new float[] { 20, 20, 20, 20, 20 };
    float[] tier3 = new float[] { 25, 25, 25, 25 };
    float[] tier4 = new float[] { 100 / 3, 100 / 3, 100 / 3 };
    float[] tier5 = new float[] { 50, 50 };

    public Player()
    {
        level = 1;
        gold = 60;
        exp = 0;
    }

    private Text goldButton = null;

    public void Start()
    {
        goldButton = GameObject.Find("Gold").GetComponentInChildren<Text>();
    }

    public void Update()
    {
        goldButton.text = gold.ToString();
    }

    public int getLevel() { return level; }

    public float[] getRatesForUnitTier(int unitTier)
    {
        switch (unitTier)
        {
            case 1:
                return tier1;
            case 2:
                return tier2;
            case 3:
                return tier3;
            case 4:
                return tier4;
            case 5:
                return tier5;
            default:
                return null;
        }
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
            activeUnitObject.GetComponent<Image>().color = Color.white;
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
    }
}