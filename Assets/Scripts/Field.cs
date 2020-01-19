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
        timer.text = Mathf.FloorToInt(HexGM.getRoundTimer()).ToString();
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

    public void switchRounds()
    {
        if (HexGM.isShoppingRound())
        {
            roundText.text = "Shopping";
            if (isShowing)
            {
                enemy.transform.localScale = Vector3.zero;
            }
            endBattleRoundTasks();
        }
        else
        {
            ally.lockBoard();
            battlefield.initAllUnits();
            if (p.getActiveUnit() != null)
            {
                p.activeUnitObject.GetComponent<Image>().color = Color.white;
                p.clearActiveUnit();
            }
            roundText.text = "Battle";
            if (isShowing)
            {
                enemy.transform.localScale = Vector3.one;
            }

        }
    }

    private void endBattleRoundTasks()
    {
        p.addInterest();
        rbh.rerollShop(0);
        p.gainExp(1);
        ally.revertToLocked();
    }
}
