using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGM : MonoBehaviour
{
    public List<Player> players;

    private static bool isBuyRound = true;
    private static float roundTimeLeft = 0.0f;
    public const float roundTimer = 15;

    void Start()
    {
        foreach (var g in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(g.GetComponent<Player>());
        }
        roundTimeLeft = roundTimer;
    }

    void Update()
    {
        roundTimeLeft -= Time.deltaTime;
        if (roundTimeLeft < 0)
        {
            switchRounds();
            roundTimeLeft = roundTimer;
        }
    }

    void switchRounds()
    {
        isBuyRound = !isBuyRound;
        foreach (Player p in players)
        {
            p.switchRounds();
        }
    }


    public static bool isShoppingRound()
    {
        return isBuyRound;
    }

    public static float getRoundTimer()
    {
        return roundTimeLeft;
    }
}
