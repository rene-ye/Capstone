using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonHandler : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;
    public Player player;
    public Text cost;

    public BenchManager benchManager;

    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }

    public void setUnit(Unit u)
    {
        unit = u;
        cost.text = u.cost.ToString();
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getSprite(unit.unit_name);
    }

    public void buyUnit()
    {
        if (unit != null && player.canAfford(unit.cost) && benchManager.addToBench(unit))
        {
            player.spend(unit.cost);
            resetDefault();
        }
    }

    private void resetDefault()
    {
        this.unit = null;
        cost.text = "";
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getDefaultShop();
    }
}