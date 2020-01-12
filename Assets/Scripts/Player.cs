using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Slider slider;
    public Text levelTextBox;

    int level, gold, exp;
    float[] tier1 = new float[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    float[] tier2 = new float[] { 20, 20, 20, 20, 20 };
    float[] tier3 = new float[] { 25, 25, 25, 25 };
    float[] tier4 = new float[] { 100 / 3, 100 / 3, 100 / 3 };
    float[] tier5 = new float[] { 50, 50 };

    static int[] expRequired = new int[] { 1, 2, 4, 8, 12, 16, 20, 24, 32, 40 };
    public const int MAX_LEVEL = 10;

    public Player()
    {
        level = 1;
        gold = 5000;
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
            levelTextBox.text = this.level.ToString();
            this.exp -= required;
            required = expRequired[level - 1];
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
}