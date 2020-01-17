using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGM : MonoBehaviour
{
    public GameObject shop;
    public Board ally, enemy;
    public RefreshButtonHandler rbh;
    public Player p;
    public const float roundTimer = 8;

    private bool isShowing = true;
    private static bool isBuyRound = true;

    private float roundTimeLeft = 0.0f;
    private Text timerText = null;
    private Text roundText = null;

    const string battleText = "Battle ends in: ";
    const string shopText = "Shop ends in: ";

    void Start()
    {
        setState();
        enemy.transform.localScale = Vector3.zero;
        roundTimeLeft = roundTimer;
        timerText = GameObject.Find("Timer").GetComponentInChildren<Text>();
        roundText = GameObject.Find("Round").GetComponentInChildren<Text>();
    }

    void Update()
    {
        roundTimeLeft -= Time.deltaTime;
        timerText.text = Mathf.FloorToInt(roundTimeLeft).ToString();
        if (roundTimeLeft < 0)
        {
            switchRounds();
        }
    }

    public void setState()
    {
        isShowing = !isShowing;
        if (isShowing)
        {
            shop.transform.localScale = Vector3.zero;
            ally.transform.localScale = Vector3.one;
            if (!isBuyRound)
            {
                enemy.transform.localScale = Vector3.one;
            }
        } else
        {
            shop.transform.localScale = Vector3.one;
            ally.transform.localScale = Vector3.zero;
            if (!isBuyRound)
            {
                enemy.transform.localScale = Vector3.zero;
            }
        }
    }

    private void switchRounds()
    {
        isBuyRound = !isBuyRound;
        if (isBuyRound)
        {
            roundText.text = shopText;
            if (isShowing)
            {
                enemy.transform.localScale = Vector3.zero;
            }
            endBattleRoundTasks();
        } else
        {
            ally.lockBoard();
            Battlefield.initAllUnits();
            if (p.getActiveUnit() != null)
            {
                p.activeUnitObject.GetComponent<Image>().color = Color.white;
                p.clearActiveUnit();
            }
            roundText.text = battleText;
            if (isShowing)
            {
                enemy.transform.localScale = Vector3.one;
            }
            
        }
        roundTimeLeft = roundTimer;
    }

    public static bool isShoppingRound()
    {
        return isBuyRound;
    }

    private void endBattleRoundTasks()
    {
        p.addInterest();
        rbh.rerollShop(0);
        p.gainExp(1);
        ally.revertToLocked();
    }
}
