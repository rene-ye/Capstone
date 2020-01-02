using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BenchButtonHandler : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;
    public Player player;

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
    }

    public bool isEmpty()
    {
        return (unit == null);
    }

    private void resetDefault()
    {
        this.unit = null;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getDefaultBench();
    }

    public void onClick()
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
