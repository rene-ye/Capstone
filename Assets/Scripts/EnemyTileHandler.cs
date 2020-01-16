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
        }
    }

    private void activateBars(bool b)
    {
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
}
