using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    List<BaseTileHandler> tiles = new List<BaseTileHandler>();
    public Player player;
    public Unit[] lockedTiles;
    public SupplyManager supply;
    public List<UnitInfo> unitInfo = new List<UnitInfo>();
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
        int unitOverflow = supply.getOverflow();
        if (unitOverflow > 0)
        {
            foreach (BaseTileHandler b in tiles)
            {
                if (b.getCurrentUnit() != null)
                {
                    player.gain(b.getCurrentUnit().cost);
                    b.resetDefault();
                    unitOverflow--;
                    supply.alterCurrentSupply(-1);
                }
                if (unitOverflow == 0)
                    break;
            }
        }
        unitInfo.Clear();
        for (int i = 0; i < tiles.Count; i++)
        {
            Unit u = tiles[i].getCurrentUnit();
            if (u != null)
            {
                unitInfo.Add(new UnitInfo(u.unit_name, u.tier, tiles[i].getCoordinate().x, tiles[i].getCoordinate().y));
            }
        }
    }

    internal void clear()
    {
        foreach(BaseTileHandler b in tiles)
        {
            b.resetDefault();
        }
    }

    public void setBoard(Unit[] u)
    {
        if (u == null || u.Length != tiles.Count)
            return;

        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            if (u[i] == null)
            {
                tiles[i].resetDefault();
            } else
            {
                Unit temp = u[i];
                temp.isAlly = false;
                tiles[i].setUnit(temp);
            }
        }
    }

    public int getTotalActiveUnits()
    {
        int i = 0;
        foreach (BaseTileHandler b in tiles)
        {
            if (b.getCurrentUnit() != null)
            {
                i++;
            }
        }
        return i;
    }
}
