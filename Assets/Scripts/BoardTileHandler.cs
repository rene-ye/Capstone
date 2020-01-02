using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardTileHandler : MonoBehaviour
{
    public Player player;
    private Unit unit = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        if (HexGM.isShoppingRound())
        {
            if (unit != null)
            {
                player.SetActiveUnit(unit, this.gameObject);
                player.rgo = new Player.ResetGameObject(resetDefault);
            }
            else if (player.getActiveUnit() != null)
            {
                if (player.activeUnitObject == this.gameObject)
                {
                    player.clearActiveUnit();
                    player.activeUnitObject.GetComponent<Image>().color = Color.white;
                    player.rgo();
                }

                setUnit(player.getActiveUnit());
                player.clearActiveUnit();
                player.activeUnitObject.GetComponent<Image>().color = Color.white;
                player.rgo();
            }
        }
    }

    private void setUnit(Unit u)
    {
        unit = u;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getSprite(unit.unit_name);
    }

    private void resetDefault()
    {
        this.unit = null;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getDefaultBench();
    }
}
