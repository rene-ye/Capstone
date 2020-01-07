using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RefreshButtonHandler : MonoBehaviour
{
    private const int REROLL_COST = 2;

    public Player p;
    public float AlphaThreshold = 0.1f;
    public Button shopButton1, shopButton2, shopButton3, shopButton4, shopButton5;

    private Button[] shopButtons;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        shopButtons = new Button[] { shopButton1, shopButton2, shopButton3, shopButton4, shopButton5 };
        foreach (Button b in shopButtons)
        {
            b.GetComponent<ShopButtonHandler>().setUnit(UnitFactory.rollUnit(p));
        }
    }

    public void rerollShop(int cost)
    {
        if (p.canAfford(cost))
        {
            p.spend(cost);
            foreach (Button b in shopButtons)
            {
                b.GetComponent<ShopButtonHandler>().setUnit(UnitFactory.rollUnit(p));
            }
        }
    }
}
