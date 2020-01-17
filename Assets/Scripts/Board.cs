using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    List<BaseTileHandler> tiles = new List<BaseTileHandler>();
    Unit[] lockedTiles;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            tiles.Add(child.GetComponent<BaseTileHandler>());
        }
        lockedTiles = new Unit[tiles.Count];
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

    public void lockBoard()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            lockedTiles[i] = tiles[i].getCurrentUnit();
        }
    }

    public void revertToLocked()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (lockedTiles[i] == null)
            {
                tiles[i].resetDefault();
            } else
            {
                tiles[i].setUnit(lockedTiles[i]);
            }
        }
    }
}
