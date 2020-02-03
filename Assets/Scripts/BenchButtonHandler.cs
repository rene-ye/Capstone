using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BenchButtonHandler : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;
    public Player player;
    public Board ally;
    public SupplyManager supply;

    public GameObject bench, trash;

    private Unit unit = null;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUnit(Unit u)
    {
        unit = u;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getSprite(unit.unit_name);
        this.gameObject.GetComponent<Image>().color = unit.getTierColor();
    }

    public bool isEmpty()
    {
        return (unit == null);
    }

    private void resetDefault()
    {
        this.unit = null;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getDefaultBench();
        this.gameObject.GetComponent<Image>().color = Color.white;
    }

    public void onClick()
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
        supply.setCurrentSupply(ally.getTotalActiveUnits());
    }
}
