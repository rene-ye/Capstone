using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RefreshButtonHandler : MonoBehaviour
{
    private const int REROLL_COST = 2;

    public GameObject playerObj;
    public float AlphaThreshold = 0.1f;
    public Button shopButton1, shopButton2, shopButton3, shopButton4, shopButton5;

    private Button[] shopButtons;
    private Player p;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        PlayerInfo pi = playerObj.GetComponent<PlayerInfo>();
        p = pi.getPlayer();

        shopButtons = new Button[] { shopButton1, shopButton2, shopButton3, shopButton4, shopButton5 };
    }

    public void rerollShop()
    {
        if (p.canAfford(REROLL_COST))
        {
            p.spend(REROLL_COST);
            foreach (Button b in shopButtons)
            {
                b.GetComponent<ShopButtonHandler>().setUnit(UnitFactory.rollUnit(p));
            }
        }
    }
}
