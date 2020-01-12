using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardTileHandler : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;
    public Player player;
    private Unit unit = null;

    private AllyBoard ally;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        ally = transform.parent.GetComponent<AllyBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!HexGM.isShoppingRound())
        {
            // unit attack nearest target
        }
    }

    public void onClick()
    {
        if (HexGM.isShoppingRound())
        {
            if (unit != null)
            {
                if (GameObject.ReferenceEquals(player.activeUnitObject, this.gameObject)) {
                    // deselect
                    player.clearActiveUnit();
                    this.gameObject.GetComponent<Image>().color = unit.getTierColor();
                } else
                {
                    player.SetActiveUnit(unit, this.gameObject);
                    player.rgo = new Player.ResetGameObject(resetDefault);
                }
            }
            else if (player.getActiveUnit() != null)
            {
                setUnit(player.getActiveUnit());
                player.clearActiveUnit();
                player.rgo();
            }
        }
    }

    private void setUnit(Unit u)
    {
        ally.checkBoardForThreeUnits(u);
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
}
