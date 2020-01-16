using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void checkBoardForThreeUnits(Unit u)
    {
        if (u.tier < 3)
        {
            List<Transform> sameUnits = new List<Transform>();
            foreach (Transform child in transform)
            {
                // iterates through all tiles and checks for Unit of the same tier
                if (Unit.sameType(child.GetComponent<BoardTileHandler>().getCurrentUnit(), u) && !GameObject.ReferenceEquals(child.GetComponent<BoardTileHandler>().getCurrentUnit(), u))
                {
                    sameUnits.Add(child);
                }
            }

            // found at least 3 units, combine first 3
            if (sameUnits.Count >= 2)
            {
                sameUnits[0].GetComponent<BoardTileHandler>().resetDefault();
                sameUnits[1].GetComponent<BoardTileHandler>().resetDefault();
                u.rankUp();
                checkBoardForThreeUnits(u);
            }
        }
    }

    public void initAllUnits()
    {
        foreach(Transform child in transform)
        {
            BoardTileHandler b = child.gameObject.GetComponent<BoardTileHandler>();
            if (b != null)
            {
                Unit u = b.getCurrentUnit();
                if (u != null)
                {
                    u.resetForCombat();
                }
            }
        }
    }
}
